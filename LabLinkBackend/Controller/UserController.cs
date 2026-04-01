using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
 [Authorize(Roles = "Admin")]   

public class UserController : ControllerBase
{
    IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserRegisterDTO userRegisterDTO)
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
                createdUser.CreatedOn
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
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO userUpdateDTO)
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
                updatedUser.Email,
                updatedUser.Phone,
                updatedUser.IsActive,
                updatedUser.UpdatedOn
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
        var isDeleted = await _userService.DeleteUser(id);
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

