using Data.Entities;

namespace Data.Repositories;

public interface IAuthorsRepository
{
    Task<Author?> GetAuthorAsync(long id);
}