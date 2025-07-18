using Core.Common;
using Core.Dtos;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicationController : Controller
{
    private readonly IPublicationService _publicationService;
    private readonly ILogger<PublicationController> _logger;

    public PublicationController(
        IPublicationService publicationService,
        ILogger<PublicationController> logger)
    {
        _publicationService = publicationService;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] CreatePublicationDto publication)
    {
        var result = await _publicationService.CreatePublicationAsync(publication);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to create publication: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        return CreatedAtAction(nameof(Create), result.Value);
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] UpdatePublicationDto dto)
    {
        var result = await _publicationService.UpdatePublicationAsync(id, dto);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to update publication {Id}: {Error}", id, result.Error);
            return BadRequest(new { error = result.Error });
        }

        if (result.Value == null)
            return NotFound();

        return Ok(result.Value);
    }

}