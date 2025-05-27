using Core.Dtos.LiteratureDirection;
using Data.Entities;

namespace Core.Interfaces.Services;

public interface ILiteratureDirectionService
{
    Task<LiteratureDirection> CreateAsync(LiteratureDirectionCreateDto dto);
}