using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using LabLinkBackend.Services;
using LabLinkBackend.DTO;
using Microsoft.AspNetCore.Authorization;
namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{

    IUserService _userService;
    private readonly IAuditLogService _auditLogService;
    
    public UserController(IUserService userService, IAuditLogService auditLogService)
    {
        _userService = userService;
        _auditLogService = auditLogService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Create(UserRegisterDTO userRegisterDTO)
    {
        if (userRegisterDTO == null)
            return BadRequest(new { message = "User registration data is required." });

        try
        {
            var result = await _userService.CreateUser(userRegisterDTO);
            var userId = (result as dynamic)?.data?.UserId;
            var name = (result as dynamic)?.data?.Name;
            var email = (result as dynamic)?.data?.Email;
            if (userId != null && name != null && email != null)
            {
                var auditDto = new AuditDto
                {
                    UserId = userId,
                    Action = "User Registered",
                    Resource = "User",
                    Metadata = $"User {name} registered with email {email}"
                };
                await _auditLogService.CreateLogAsync(auditDto);
            }
            return StatusCode(201, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to create user", detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
    {
        if (userUpdateDTO == null)
            return BadRequest(new { message = "User update data is required." });

        try
        {
            var result = await _userService.UpdateUserAndRoles(id, userUpdateDTO);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to update user and roles", detail = ex.Message });
        }
    }
}