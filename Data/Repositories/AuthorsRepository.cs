using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public AuthorsRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Author Create(
        Author entity)
    {
        throw new NotImplementedException();
    }

    public Task<Author> CreateAsync(
        Author entity)
    {
        throw new NotImplementedException();
    }

    public Task CreateRangeAsync(
        IEnumerable<Author> items)
    {
        throw new NotImplementedException();
    }

    public EntityEntry<Author> Update(
        Author entity)
    {
        throw new NotImplementedException();
    }

    public void UpdateRange(
        IEnumerable<Author> items)
    {
        throw new NotImplementedException();
    }

    public void Delete(
        Author entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(
        IEnumerable<Author> items)
    {
        throw new NotImplementedException();
    }

    public async Task<Author?> GetAuthorAsync(long id)
    {
        return await _dbContext.Authors.Where(w => w.Id == id).FirstOrDefaultAsync();
    }
}