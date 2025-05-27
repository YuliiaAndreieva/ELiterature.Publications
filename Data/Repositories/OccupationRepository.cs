using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class OccupationRepository : IOccupationRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public OccupationRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<Occupation> GetAllAsync()
    {
        return _dbContext.Occupations;
    }

    public async Task<List<Occupation>> GetOccupationsByIdAsync(List<long> ids)
    {
        return await _dbContext.Occupations
            .Where(d => ids.Contains(d.Id))
            .ToListAsync();
    }
    
    public async Task CreateAsync(Occupation occupation)
    {
        _dbContext.Occupations.Add(occupation);
        await _dbContext.SaveChangesAsync();
    }
}