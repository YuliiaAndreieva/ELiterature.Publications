using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[Authorize]
[ApiController]
public class WritersController : ControllerBase
{
    private readonly IWritersService _writersService;

    public WritersController(
        IWritersService writersService)
    {
        _writersService = writersService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWriter(long id)
    {
        var writer = await _writersService.GetWriterByIdAsync(id);

        if (writer is null)
            return NotFound();

        return Ok(writer);
    }
}