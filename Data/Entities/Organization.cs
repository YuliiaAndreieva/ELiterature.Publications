namespace Data.Entities;

public class Organization
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public IEnumerable<Writer> Writers { get; set; } = new List<Writer>();
}