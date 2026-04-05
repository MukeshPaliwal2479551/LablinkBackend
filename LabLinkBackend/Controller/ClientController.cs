using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ClientListResult>> GetAll()
    {
        var res = await _clientService.GetAllAsync();
        if (!res.Result)
        {
            return StatusCode(500, res);
        }
        return Ok(res);
    }
}
