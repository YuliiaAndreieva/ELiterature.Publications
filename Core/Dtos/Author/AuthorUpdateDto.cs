using Core.Dtos.Photo;

namespace Core.Dtos.Author;

public class AuthorUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? DateOfDeath { get; set; }
    public string Biography { get; set; }
    public List<long> DirectionIds { get; set; }

    public List<long> OccupationIds { get; set; }
    public List<long> PublicationIds { get; set; }
    
    public List<AuthorPhotoDto> Photos { get; set; }
}