using Core.Dtos;
using Core.Dtos.Moodboard;
using Core.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicationController : Controller
{
    private readonly IPublicationService _publicationService;

    public PublicationController(
        IPublicationService publicationService)
    {
        _publicationService = publicationService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Publication>> GetAllPublications()
    {
        var publications = _publicationService.GetAllPublicationsAsync();
        return Ok(publications);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePublicationDto publication)
    {
        var created = await _publicationService.CreateAsync(publication);
        return CreatedAtAction(nameof(Create), created);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdatePublicationDto dto)
    {
        var updated = await _publicationService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpGet("moodboard")]
    public async Task<ActionResult<IEnumerable<PublicationMoodboardDto>>> GetMoodboardPublications(
        [FromQuery] int count = 6)
    {
        try
        {
            var publications = await _publicationService.GetRandomPublicationsForMoodboardAsync(count);
            return Ok(publications);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Помилка при отриманні публікацій для moodboard");
        }
    }
}