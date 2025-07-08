using Core.Dtos.Photo;
using Data.Entities.Enums;

namespace Core.Dtos;

public class CreatePublicationDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateOnly? PublicationYear { get; set; }
    public PublicationType Type { get; set; }
    public required string Text { get; set; }

    public List<long> AuthorIds { get; set; } = new();
    public List<long> DirectionIds { get; set; } = new();
    public List<long> TagIds { get; set; } = new();
    public List<PublicationPhotoDto> Photos { get; set; } = new();
}