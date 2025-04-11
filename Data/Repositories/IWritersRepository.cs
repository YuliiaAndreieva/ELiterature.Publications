using Data.Entities;

namespace Data.Repositories;

public interface IWritersRepository
{
    Task<Writer?> GetWriterAsync(long id);
}