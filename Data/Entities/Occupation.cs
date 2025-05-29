namespace Data.Entities;

public class Occupation
{
    public long Id { get; set; } 
    
    public required string Title { get; set; } 
    
    public IEnumerable<Author> Authors { get; set; } = new List<Author>();
}