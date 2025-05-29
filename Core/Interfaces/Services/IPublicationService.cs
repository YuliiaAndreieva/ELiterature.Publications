using Core.Dtos;

namespace Core.Interfaces.Services;

public interface IPublicationService
{
    Task<UpdatePublicationDto?> UpdateAsync(long id, UpdatePublicationDto dto);

    Task<CreatePublicationDto> CreateAsync(CreatePublicationDto dto);
}