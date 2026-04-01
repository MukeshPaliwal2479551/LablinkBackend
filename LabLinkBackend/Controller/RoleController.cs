using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LabLinkBackend.Services;
using Role.DTO;

[ApiController]
[Route("api/[controller]")]
 [Authorize(Roles = "Admin")]   
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole(RoleDto dto)
    {
        var result = await _service.CreateRoleAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _service.GetAllRolesAsync();
        return Ok(roles);
    }
}