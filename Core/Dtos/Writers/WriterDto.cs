using System.Text.Json.Serialization;
using Core.Dtos.Occupation;
using Core.Dtos.Photo;

namespace Core.Dtos.Writers;

public class WriterDto
{
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }
    
    public PhotoDto MainImage { get; set; }

    [JsonPropertyName("occupations")]
    public IEnumerable<OccupationDto> OccupationDtos { get; set; }
    
    [JsonPropertyName("photos")]
    public IEnumerable<WriterPhotoDto> Photos { get; set; } = new List<WriterPhotoDto>();
}