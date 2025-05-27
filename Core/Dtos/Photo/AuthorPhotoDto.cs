using Data.Entities.Enums;

namespace Core.Dtos.Photo;

public class AuthorPhotoDto
{
    public string PhotoUrl { get; set; }

    public long Id { get; set; }

    public PhotoType Type { get; set; }

    public string? Quote { get; set; }
}