using Core.Dtos.LiteratureDirection;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.Services;

public class LiteratureDirectionService : ILiteratureDirectionService
{
    private readonly ILiteratureDirectionRepository _directionRepository;

    public LiteratureDirectionService(
        ILiteratureDirectionRepository directionRepository)
    {
        _directionRepository = directionRepository;
    }

    public async Task<LiteratureDirection> CreateAsync(LiteratureDirectionCreateDto dto)
    {
        var direction = new LiteratureDirection
        {
            Title = dto.Title,
            Description = dto.Description,
            StartCentury = dto.StartCentury,
            EndCentury = dto.EndCentury
        };

        await _directionRepository.CreateAsync(direction);
        return direction;
    }
}