using Data.Entities;

namespace Data.Repositories.Interfaces;
public interface IAuthorsRepository
{
    IQueryable<Author> GetByIdAsyncAsQueryable(long id);

    Task<Author?> GetByIdAsync(long id);

    IQueryable<Author> GetAllAsync();
    
    Task<Author> CreateAsync(Author author);

    Task<Author?> UpdateAsync(Author author);

    Task DeleteAsync(Author author);

    IEnumerable<Author> GetAllAsyncAsEnumerable();

    Task<List<Author>> GetAuthorsByIdsAsync(List<long> ids);
}