using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories;

public class WritersRepository : IWritersRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public WritersRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Writer Create(
        Writer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Writer> CreateAsync(
        Writer entity)
    {
        throw new NotImplementedException();
    }

    public Task CreateRangeAsync(
        IEnumerable<Writer> items)
    {
        throw new NotImplementedException();
    }

    public EntityEntry<Writer> Update(
        Writer entity)
    {
        throw new NotImplementedException();
    }

    public void UpdateRange(
        IEnumerable<Writer> items)
    {
        throw new NotImplementedException();
    }

    public void Delete(
        Writer entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(
        IEnumerable<Writer> items)
    {
        throw new NotImplementedException();
    }

    public async Task<Writer?> GetWriterAsync(long id)
    {
        return await _dbContext.Writers.Where(w => w.Id == id).FirstOrDefaultAsync();
    }
}