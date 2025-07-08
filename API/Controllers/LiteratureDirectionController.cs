using Core.Dtos.LiteratureDirection;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LiteratureDirectionController : ControllerBase
{
    private readonly ILiteratureDirectionService _service;

    public LiteratureDirectionController(ILiteratureDirectionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LiteratureDirectionCreateDto dto)
    {
        var direction = await _service.CreateAsync(dto);
        return Ok(direction);
    }
}