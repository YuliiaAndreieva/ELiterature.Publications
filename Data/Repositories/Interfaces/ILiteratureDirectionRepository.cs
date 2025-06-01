using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface ILiteratureDirectionRepository
{
    IQueryable<LiteratureDirection> GetAllAsync();
    Task<List<LiteratureDirection>> GetDirectionsByIdsAsync(List<long> ids);

    Task CreateAsync(LiteratureDirection direction);

    IQueryable<LiteratureDirection> GetByIdAsyncAsQueryable(
        long id);
}