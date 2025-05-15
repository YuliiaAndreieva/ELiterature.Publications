using Data.Entities;

namespace Data.Repositories;

public interface IAuthorsRepository
{
    IQueryable<Author> GetAuthorByIdAsync(long id);

    IQueryable<Author> GetAllAsync();
}