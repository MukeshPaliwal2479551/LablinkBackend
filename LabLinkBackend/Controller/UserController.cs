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
            var createdUser = await _userService.CreateUser(userRegisterDTO);

            var auditDto = new AuditDto
            {
                UserId = createdUser.UserId,
                Action = "User Registered",
                Resource = "User",
                Metadata = $"User {createdUser.Name} registered with email {createdUser.Email}"
            };
            await _auditLogService.CreateLogAsync(auditDto);

            var response = new
            {
                createdUser.UserId,
                createdUser.Name,
                createdUser.Email,
                createdUser.Phone,
                createdUser.IsActive,
                createdUser.CreatedOn,
                Roles = createdUser.UserRoles?.Select(ur => new
                {
                    ur.RoleId,
                    RoleName = ur.Roles?.Role
                }).ToList()
            };

            return StatusCode(201, new {message= "user created", data= response});
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
            var updatedUser = await _userService.UpdateUserAndRoles(id, userUpdateDTO);

            var response = new
            {
                updatedUser.UserId,
                updatedUser.Name,
                updatedUser.Phone,
                updatedUser.IsActive,
                updatedUser.UpdatedOn,
                updatedUser.Email,
                Roles = updatedUser.UserRoles?.Select(ur => new
                {
                    ur.RoleId,
                    RoleName = ur.Roles?.Role
                }).ToList()
            };

            return Ok(new { message = "User and roles updated successfully", data = response });
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