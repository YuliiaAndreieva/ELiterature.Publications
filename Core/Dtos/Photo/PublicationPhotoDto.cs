using Data.Entities.Enums;

namespace Core.Dtos.Photo;

public class PublicationPhotoDto
{
    public string PhotoUrl { get; set; }

    public long Id { get; set; }

    public PhotoType Type { get; set; }
    
}