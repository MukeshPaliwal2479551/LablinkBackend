using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LabLinkBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }     
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid user id. Id must be greater than zero."
                });
            }
            var isDeleted = await _service.DeleteUser(id);
            if (!isDeleted)
            {
                return NotFound(new
                {
                    Message = "User not found"
                });
            }
            return Ok(new
            {
                Message = "User deleted successfully"
            });
        }
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUsers(string? name, string? phone)
        {
            var users = await _service.GetUsersAsync(name, phone);
            if (users == null || !users.Any())
            {
                return NotFound(new
                {
                    Message = "No users found"
                });
            }
            return Ok(users);
        }
    }
}
