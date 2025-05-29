using Core.Interfaces.Services;
using Data.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;
    
    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, [FromForm] PhotoType type)
    {
        var result = await _photoService.AddPhotoAsync(file);
        if (result is null) return BadRequest("Upload failed");
        return Ok(new { url = result.SecureUrl.ToString(), publicId = result.PublicId, type });
    }
}