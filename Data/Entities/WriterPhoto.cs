namespace Data.Entities;

public class WriterPhoto : Photo
{
    public long WriterId { get; set; }
    
    public Writer Writer { get; set; } = null!;
    
    public string? Quote { get; set; }
}