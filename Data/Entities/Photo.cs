using Data.Entities.Enums;

namespace Data.Entities;

public class Photo
{
    public long Id { get; set; }
    
    public PhotoType Type { get; set; }
    
    public string PhotoUrl { get; set; } = string.Empty;
}