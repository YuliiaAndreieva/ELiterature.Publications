using Data.Entities.Enums;

namespace Core.Dtos.Photo;

public class PhotoDto
{
    public PhotoType Type { get; set; }
    
    public string PhotoUrl { get; set; }
}