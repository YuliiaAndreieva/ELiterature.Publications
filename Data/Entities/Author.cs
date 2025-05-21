namespace Data.Entities;

public class Author
{
    public long Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string MiddleName { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
    
    public DateOnly? DateOfDeath { get; set; } 
    
    public string Biography { get; set; } = string.Empty;

    public  ICollection<Publication> Publications { get; set; }

    public IEnumerable<LiteratureDirection> LiteratureDirection { get; set; } = new List<LiteratureDirection>();

    public IEnumerable<Occupation> Occupations { get; set; } = new List<Occupation>();

    public IEnumerable<Organization> Organizations { get; set; } = new List<Organization>();
    
    public IEnumerable<AuthorPhoto> Photos { get; set; } = new List<AuthorPhoto>();
}