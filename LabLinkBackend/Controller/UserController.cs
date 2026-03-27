using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;

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
}

