using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
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
                    RoleName = ur.Role?.Role1
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
            var updatedUser = await _userService.UpdateUser(id, userUpdateDTO);

            var response = new
            {
                updatedUser.UserId,
                updatedUser.Name,
                updatedUser.Phone,
                updatedUser.IsActive,
                updatedUser.UpdatedOn,
                updatedUser.Email
            };

            return Ok(new { message = "User updated successfully", data = response });
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
            return StatusCode(500, new { message = "Failed to update user", detail = ex.Message });
        }
    }

    [HttpPut("{id}/roles")]
    public async Task<IActionResult> UpdateRoles(int id, [FromBody] UserRoleUpdateDTO roleUpdateDTO)
    {
        if (roleUpdateDTO == null)
            return BadRequest(new { message = "Role update data is required." });

        try
        {
            var updatedUser = await _userService.UpdateUserRoles(id, roleUpdateDTO.RoleIds);

            var response = new
            {
                updatedUser.UserId,
                Roles = updatedUser.UserRoles?.Select(ur => new
                {
                    ur.RoleId,
                    RoleName = ur.Role?.Role1
                }).ToList()
            };

            return Ok(new { message = "User roles updated successfully", data = response });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to update user roles", detail = ex.Message });
        }
    }
}

