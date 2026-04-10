using LabLinkBackend.DTO;
using LabLinkBackend.Services;
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
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserRegisterDTO userRegisterDTO)
        {
            
            if (userRegisterDTO == null)
                return BadRequest(new { message = "User registration data is required." });
            
            // Only allow unauthenticated registration for patient role (roleId == 1)
            var isPatientOnly = userRegisterDTO.RoleIds.Count == 1 && userRegisterDTO.RoleIds.Contains(1);
            if (!isPatientOnly && (!(User?.Identity?.IsAuthenticated ?? false)))
            {
                return StatusCode(403, new { message = "Only authenticated admins can register non-patient users." });
            }

            // If not patient, require admin role
            if (!isPatientOnly && !(User?.IsInRole("Admin") ?? false))
            {
                return StatusCode(403, new { message = "Only admins can register non-patient users." });
            }

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

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (userUpdateDTO == null)
                return BadRequest(new { message = "User update data is required." });

            // Only allow admin to update roles
            if (userUpdateDTO.RoleIds != null && userUpdateDTO.RoleIds.Any() && !(User?.IsInRole("Admin") ?? false))
            {
                return StatusCode(403, new { message = "Only admins can update user roles." });
            }

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


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid user id." });
            }

            if (!(User?.IsInRole("Admin") ?? false))
            {
                return StatusCode(403, new { message = "Only admins can update user roles." });
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
 
 
 
