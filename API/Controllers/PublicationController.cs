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

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var publications =  _publicationService.GetAllPublicationsAsync();
            return Ok(publications);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all publications");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        try
        {
            var publication = await  _publicationService.GetPublicationByIdAsync(id);
            
            if (publication.Value is null)
                return NotFound();

            return Ok(publication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting publication {Id}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
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