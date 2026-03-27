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
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
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
            {
                return BadRequest(new { message = "Invalid Client Request" });
            }

            // 1. Fetch User
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized(new { message = "Invalid Email" });

            // 2. Fetch UserRole and Role
            var userRole = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.UserId).ToListAsync();

            if (userRole == null)
                return Unauthorized(new { message = "User role not assigned" });

            // 3. Check password
            if (request.Password != user.Password)
                return Unauthorized(new { message = "Invalid password" });
              var roleIds = userRole.Select(e=>e.RoleId);
            // 4. Generate JWT Token
            var tokenString = GenerateJwtToken(user, roleIds);

            return Ok(new
            {
                token = tokenString,
                userId = user.UserId,
                roleId = userRole.Select(e=>e.RoleId)
            });
        }

        private string GenerateJwtToken(User user, IEnumerable<int> roleId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("userId", user.UserId.ToString()),
                new Claim("roleId", roleId.ToString())
            };

            var jwtKey = _configuration["Jwt:Key"]
                         ?? throw new InvalidOperationException("JWT Key not configured");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}