using Core.Dtos.Occupation;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.Services;

public class OccupationService : IOccupationService
{
    private readonly IOccupationRepository _occupationRepository;

    public OccupationService(
        IOccupationRepository occupationRepository)
    {
        _occupationRepository = occupationRepository;
    }

    public async Task<OccupationCreateDto> CreateAsync(
        OccupationCreateDto dto)
    {
        var occupation = new Occupation()
        {
            Title = dto.Title
        };

        var entity = await  _occupationRepository.CreateAsync(occupation);
        return new OccupationCreateDto() {Id = entity.Id, Title = entity.Title};
    }
}