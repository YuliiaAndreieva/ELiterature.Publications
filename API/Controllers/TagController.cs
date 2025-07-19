using Core.Dtos;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : Controller
{
    private readonly ITagService _tagService;
    private readonly ILogger<TagController> _logger;

    public TagController(ITagService tagService, ILogger<TagController> logger)
    {
        _tagService = tagService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagCreateDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("TagCreateDto is null");
                return BadRequest(new { error = "Tag data cannot be null" });
            }

            var result = await _tagService.CreateAsync(dto);
            if (result == null)
            {
                _logger.LogWarning("TagService.CreateAsync returned null");
                return BadRequest(new { error = "Failed to create tag" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating tag");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}