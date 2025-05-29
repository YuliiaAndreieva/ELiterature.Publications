namespace Data.Entities;

public class AuthorPhoto : Photo
{
    public long AuthorId { get; set; }
    
    public Author Author { get; set; } = null!;
    
    public string? Quote { get; set; }
}