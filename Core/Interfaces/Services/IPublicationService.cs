using Core.Common;
using Core.Dtos;
using Core.Dtos.Moodboard;
using Data.Entities;

namespace Core.Interfaces.Services;

public interface IPublicationService
{
    Task<Result<CreatePublicationDto>> CreatePublicationAsync(CreatePublicationDto dto);

    Task<Result<UpdatePublicationDto>> UpdatePublicationAsync(
        long id,
        UpdatePublicationDto dto);

    IEnumerable<Publication> GetAllPublicationsAsync();
    
    Task<IEnumerable<PublicationMoodboardDto>> GetRandomPublicationsForMoodboardAsync(int count);
}