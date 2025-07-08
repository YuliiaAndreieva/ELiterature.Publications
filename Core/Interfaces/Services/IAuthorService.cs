using Core.Dtos.Author;
using Core.Services;
using Data.Entities;

namespace Core.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorUpdateDto?> UpdateAsync(long id, AuthorUpdateDto dto);

    Task<AuthorCreateDto> CreateAsync(AuthorCreateDto dto);

    Task<bool> DeleteAsync(long id);

    IEnumerable<AuthorSelectDto> GetAllForSelectAsync();
}