using Core.Dtos.Author;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorsService;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(IAuthorService authorsService, ILogger<AuthorsController> logger)
    {
        _authorsService = authorsService;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var list = _authorsService.GetAllForSelectAsync();
            return Ok(list.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all authors");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto author)
    {
        try
        {
            if (author == null)
            {
                _logger.LogWarning("AuthorCreateDto is null");
                return BadRequest(new { error = "Author data cannot be null" });
            }

            var created = await _authorsService.CreateAsync(author);
            if (created == null)
            {
                _logger.LogWarning("AuthorService.CreateAsync returned null");
                return BadRequest(new { error = "Failed to create author" });
            }

            return CreatedAtAction(nameof(Create), created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating author");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, [FromBody] AuthorUpdateDto dto)
    {
        try
        {
            if (dto == null)
            {
                _logger.LogWarning("AuthorUpdateDto is null");
                return BadRequest(new { error = "Author data cannot be null" });
            }

            var updated = await _authorsService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating author {Id}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var result = await _authorsService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting author {Id}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}