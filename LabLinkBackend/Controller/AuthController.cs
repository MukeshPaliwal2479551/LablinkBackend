using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LabLinkBackend.Models;
using JsonWebToken.DTO;
 
namespace LabLinkBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//forces us to use attribute routing 
    public class LoginController : ControllerBase//allows us to use ok() badrequest() unauthorized()
    {
        private readonly LabLinkDbContext _context;
        private readonly IConfiguration _configuration;
 
        public LoginController(LabLinkDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
 
        [HttpPost]
        
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            if (request == null)
                return BadRequest(new { message = "Invalid Client Request" });//400
 
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);
 
            if (user == null)
                return Unauthorized(new { message = "Invalid Email" });//401
 
            var userRoles = await _context.UserRoles//joins the role table with the junction table user role
                .Include(ur => ur.Roles)//ensures it joins the tables so you get the actual role names, not just IDs.
                .Where(ur => ur.UserId == user.UserId)
                .ToListAsync();
 
            if (!userRoles.Any())
                return Unauthorized(new { message = "User role not assigned" });
 
            if (request.Password != user.Password)
                return Unauthorized(new { message = "Invalid password" });
 
            List<string> roleNames = userRoles.Select(r => r.Roles.Role).ToList();
 
            var tokenString = GenerateJwtToken(user, roleNames);
 
            return Ok(new //200
            {
                token = tokenString,
                userId = user.UserId,
                roles = roleNames
            });
        }
 
        private string GenerateJwtToken(User user, List<string> roleNames)
        {
            var claims = new List<Claim>// creating the payload the facts abut user
            {
                // FIX: Add '?? string.Empty' to handle potential null emails
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim("userId", user.UserId.ToString()) 
            };

            foreach (var role in roleNames)
            {
                // FIX: Ensure the role isn't null or empty before creating the claim
                if (!string.IsNullOrEmpty(role)) 
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key not configured");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//the signature uses this specific maths algo which takes the payload +header + secret key and mixes them to kae a 256 bit signature
        

        /*
        This is where all the pieces come together into a C# object:
Issuer: Who created this token? (Usually your API's URL).
Audience: Who is this token meant for? (Usually your frontend application's URL, though here you've set it to the same as the issuer).
Claims: The suitcase of user data you packed in step 1.
Expires: A vital security measure. The token is only valid for 1 hour. If a hacker steals it, they only have a 60-minute window to use it.
SigningCredentials: The mathematical wax seal you created in step 3.
*/
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
 
            return new JwtSecurityTokenHandler().WriteToken(token);//converting the c# token object into standard jwt string format header payload and signature
        }
    }
}