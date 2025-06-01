using Core.Dtos;
using Core.Dtos.Moodboard;
using Data.Entities;

namespace Core.Interfaces.Services;

public interface IPublicationService
{
    Task<UpdatePublicationDto?> UpdateAsync(long id, UpdatePublicationDto dto);

    Task<CreatePublicationDto> CreateAsync(CreatePublicationDto dto);

    IEnumerable<Publication> GetAllPublicationsAsync();
    
    Task<IEnumerable<PublicationMoodboardDto>> GetRandomPublicationsForMoodboardAsync(int count);
}