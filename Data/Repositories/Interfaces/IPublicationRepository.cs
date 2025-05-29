using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IPublicationRepository
{
    IQueryable<Publication> GetAllAsync();
    Task<List<Publication>> GetPublicationsByIdsAsync(List<long> ids);
    IQueryable<Publication> GetByIdAsyncAsQueryable(long id);
    Task<Publication?> GetByIdAsync(long id);

    Task<Publication> CreateAsync(
        Publication publication);

    Task<Publication?> UpdateAsync(
        Publication author);

    Task DeleteAsync(
        Publication author);
}