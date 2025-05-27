using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IPublicationRepository
{
    Task<List<Publication>> GetPublicationsByIdsAsync(List<long> ids);
}