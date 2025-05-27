using Core.Dtos.Occupation;
using Data.Entities;

namespace Core.Interfaces.Services;

public interface IOccupationService
{
    Task<OccupationCreateDto> CreateAsync(OccupationCreateDto dto);
}