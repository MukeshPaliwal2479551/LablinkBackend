using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace LabLinkBackend.Controller;
 
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuditLogService _auditLogService;
    public UserController(IUserService userService,IAuditLogService auditLogService)
    {
        _userService = userService;
        _auditLogService=auditLogService;
    }
    [Authorize]
[HttpDelete("delete/{id}")]
public async Task<IActionResult> Delete(int id)
{
    if (id <= 0)
    {
        return BadRequest(new { Message = "Invalid user id." });
    }

    var isDeleted = await _userService.Delete(id);
    if (!isDeleted)
    {
        return NotFound(new { Message = "User not found" });
    }

    var userIdClaim = User.FindFirst("userId");
    var auditDto = new AuditDto
    {
        UserId = int.Parse(userIdClaim.Value),   // ✅ actor
        Action = "User Deleted",
        Resource = "User",
        Metadata = $"User with ID {id} was deleted"
    };

    await _auditLogService.CreateLogAsync(auditDto);

    return Ok(new { Message = "User deleted successfully" });
    }
    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUsers(string name="", string phone="")
    {
        var users = await _userService.GetUsersAsync(name, phone);
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
 
 
 