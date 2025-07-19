using Core.Dtos.LiteratureDirection;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LiteratureDirectionController : ControllerBase
{
    private readonly ILiteratureDirectionService _service;
    private readonly ILogger<LiteratureDirectionController> _logger;

    public LiteratureDirectionController(ILiteratureDirectionService service, ILogger<LiteratureDirectionController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LiteratureDirectionCreateDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("LiteratureDirectionCreateDto is null");
                return BadRequest(new { error = "Literature direction data cannot be null" });
            }

            var result = await _service.CreateAsync(dto);
            if (result == null)
            {
                _logger.LogWarning("LiteratureDirectionService.CreateAsync returned null");
                return BadRequest(new { error = "Failed to create literature direction" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating literature direction");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}