using Core.Dtos;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(
        ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    public async Task<TagCreateDto> CreateAsync(
        TagCreateDto dto)
    {
        var occupation = new Tag()
        {
            Title = dto.Title
        };

        var entity = await  _tagRepository.CreateAsync(occupation);
        return new TagCreateDto() {Id = entity.Id, Title = dto.Title};
    }
}