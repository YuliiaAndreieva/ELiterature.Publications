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

    public IQueryable<Publication> GetAllAsync()
    {
        return _dbContext.Publications;
    }
    
    public async Task<List<Publication>> GetPublicationsByIdsAsync(List<long> ids)
    {
        return await _dbContext.Publications
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
    
    public IQueryable<Publication> GetByIdAsyncAsQueryable(long id)
    {
        return _dbContext.Publications
            .Where(w => w.Id == id);
    }
    
    public async Task<Publication?> GetByIdAsync(long id)
    {
        return await _dbContext.Publications
            .Include(w => w.Authors)
            .Include(w => w.LiteratureDirection)
            .Include(w=> w.Tags)
            .Where(w => w.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Publication> CreateAsync(Publication publication)
    {
        _dbContext.Publications.Add(publication);
        await _dbContext.SaveChangesAsync();
        return publication;
    }

    public async Task<Publication?> UpdateAsync(Publication author)
    {
        _dbContext.Publications.Update(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task DeleteAsync(Publication author)
    {
        _dbContext.Publications.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}