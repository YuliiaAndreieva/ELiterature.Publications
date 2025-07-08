using Core.Dtos.Occupation;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OccupationsController : ControllerBase
{
    private readonly IOccupationService _occupationService;

    public OccupationsController(
        IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OccupationCreateDto dto)
    {
        var occupation = await _occupationService.CreateAsync(dto);
        return Ok(occupation);
    }
}