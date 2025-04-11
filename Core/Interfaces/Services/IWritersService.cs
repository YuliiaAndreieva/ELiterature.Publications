using Core.Dtos.Writers;

namespace Core.Interfaces.Services;

public interface IWritersService
{
    Task<WriterDto?> GetWriterByIdAsync(long id);
}