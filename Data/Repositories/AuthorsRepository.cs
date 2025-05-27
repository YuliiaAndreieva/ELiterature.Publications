using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public AuthorsRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Author> GetByIdAsyncAsQueryable(long id)
    {
        return _dbContext.Authors
            .Where(w => w.Id == id);
    }
    
    public async Task<Author?> GetByIdAsync(long id)
    {
        return await _dbContext.Authors.Where(w => w.Id == id).FirstOrDefaultAsync();
    }
    
    public IQueryable<Author> GetAllAsync()
    {
        return _dbContext.Authors;
    }
    
    public async Task<Author> CreateAsync(Author author)
    {
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> UpdateAsync(Author author)
    {
        _dbContext.Authors.Update(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task DeleteAsync(Author author)
    {
        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}