using Data.Entities.Enums;

namespace Core.Dtos.Writers;

public class WriterPhotoDto
{
    public long Id { get; set; }
    
    public PhotoType Type { get; set; }
    
    public string PhotoUrl { get; set; }
    
    public string? Quote { get; set; }
}