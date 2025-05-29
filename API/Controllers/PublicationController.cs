using Core.Dtos;
using Core.Interfaces.Services;
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
}