namespace Data.Entities;

public class LiteratureDirection
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public int StartCentury { get; set; }
    
    public int? EndCentury { get; set; }
    
    public IEnumerable<Author> Authors { get; set; } = new List<Author>();
    
    public IEnumerable<Publication> Publications { get; set; } = new List<Publication>();
}