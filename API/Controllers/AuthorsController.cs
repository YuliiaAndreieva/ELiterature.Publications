using Core.Dtos.Author;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorsService;

    public AuthorsController(IAuthorService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto author)
    {
        var created = await _authorsService.CreateAsync(author);
        return CreatedAtAction(nameof(Create), created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, [FromBody] AuthorUpdateDto dto)
    {
        var updated = await _authorsService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _authorsService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}