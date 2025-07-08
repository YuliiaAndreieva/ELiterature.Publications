using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Services;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

    Task<DeletionResult> DeletePhotoAsync(string publicUrl);
}