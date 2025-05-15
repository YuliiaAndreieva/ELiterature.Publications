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

    public IQueryable<Author> GetAuthorByIdAsync(long id)
    {
        return _dbContext.Authors
            .Where(w => w.Id == id);
    }
    
    public IQueryable<Author> GetAllAsync()
    {
        return _dbContext.Authors;
    }
}