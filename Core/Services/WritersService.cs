using Core.Dtos.Occupation;
using Core.Dtos.Writers;
using Core.Interfaces.Services;
using Data.Repositories;

namespace Core.Services;

public class WritersService : IWritersService
{
    private readonly IAuthorsRepository _repository;

    public WritersService(
        IAuthorsRepository repository)
    {
        _repository = repository;
    }

    public async Task<WriterDto?> GetWriterByIdAsync(
        long id)
    {
        var writerEntity = await  _repository.GetAuthorAsync(id);

        if (writerEntity is null)
            return null;

        return new WriterDto()
        {
            Id = writerEntity.Id,
            FirstName = writerEntity.FirstName,
            LastName = writerEntity.LastName,
            MiddleName = writerEntity.MiddleName,
            OccupationDtos = new List<OccupationDto>() 
        };
    }
}