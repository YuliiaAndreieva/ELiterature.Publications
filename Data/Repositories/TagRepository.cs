using Data.Context;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TagEntity = Data.Entities.Tag;
namespace Data.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ELiteratureDbContext _dbContext;

    public TagRepository(
        ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TagEntity> GetAllAsync()
    {
        return _dbContext.Tags;
    }
    
    public async  Task<TagEntity> CreateAsync(
        TagEntity tag)
    {
        _dbContext.Tags.Add(tag);
        await _dbContext.SaveChangesAsync();
        return tag;
    }
    
    public async Task<List<TagEntity>> GetTagsByIdsAsync(List<long> ids)
    {
        return await _dbContext.Tags
            .Where(d => ids.Contains(d.Id))
            .ToListAsync();
    }
}