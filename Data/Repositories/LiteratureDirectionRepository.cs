using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class LiteratureDirectionRepository : ILiteratureDirectionRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public LiteratureDirectionRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<LiteratureDirection> GetAllAsync()
    {
        return _dbContext.LiteratureDirections;
    }

    public async Task<List<LiteratureDirection>> GetDirectionsByIdsAsync(List<long> ids)
    {
        return await _dbContext.LiteratureDirections
            .Where(d => ids.Contains(d.Id))
            .ToListAsync();
    }
    
    public async Task CreateAsync(LiteratureDirection direction)
    {
        _dbContext.LiteratureDirections.Add(direction);
        await _dbContext.SaveChangesAsync();
    }
}