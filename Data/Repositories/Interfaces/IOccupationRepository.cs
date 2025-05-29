using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IOccupationRepository
{
    IQueryable<Occupation> GetAllAsync();

    Task<List<Occupation>> GetOccupationsByIdAsync(List<long> ids);

    Task<Occupation> CreateAsync(Occupation occupation);
}