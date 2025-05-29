using Core.Dtos;

namespace Core.Interfaces.Services;

public interface ITagService
{
    Task<TagCreateDto> CreateAsync(
        TagCreateDto dto);
}