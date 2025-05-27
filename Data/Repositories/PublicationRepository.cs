using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PublicationRepository : IPublicationRepository
{
    private readonly ELiteratureDbContext _dbContext;
    
    public PublicationRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Publication>> GetPublicationsByIdsAsync(List<long> ids)
    {
        return await _dbContext.Publications
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}