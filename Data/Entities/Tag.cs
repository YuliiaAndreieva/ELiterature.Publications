namespace Data.Entities;

public class Tag
{
    public long Id { get; set; } 
    
    public required string Title { get; set; } 
    
    public IEnumerable<Publication> Publications { get; set; }
}