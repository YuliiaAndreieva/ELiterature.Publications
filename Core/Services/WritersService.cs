using Core.Dtos.Writers;
using Core.Interfaces.Services;
using Data.Repositories;

namespace Core.Services;

public class WritersService : IWritersService
{
    private readonly IWritersRepository _repository;

    public WritersService(
        IWritersRepository repository)
    {
        _repository = repository;
    }

    public async Task<WriterDto?> GetWriterByIdAsync(
        long id)
    {
        var writerEntity = await  _repository.GetWriterAsync(id);

        if (writerEntity is null)
            return null;

        return new WriterDto()
        {
            Id = writerEntity.Id,
            FirstName = writerEntity.FirstName,
            LastName = writerEntity.LastName,
        };
    }
}