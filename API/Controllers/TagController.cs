using Core.Dtos;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TagController : Controller
{
    private readonly ITagService _tagService;

    public TagController(
        ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagCreateDto dto)
    {
        var occupation = await _tagService.CreateAsync(dto);
        return Ok(occupation);
    }
}