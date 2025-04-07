namespace Data.Entities;

public class LiteratureDirection
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public uint StartCentury { get; set; }
    
    public uint? EndCentury { get; set; }
    
    public IEnumerable<Writer> Writers { get; set; } = new List<Writer>();
    
    public IEnumerable<Publication> Publications { get; set; } = new List<Publication>();
}