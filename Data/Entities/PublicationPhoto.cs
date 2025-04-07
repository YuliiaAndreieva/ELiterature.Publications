namespace Data.Entities;

public class PublicationPhoto : Photo
{
    public long PublicationId { get; set; }
    
    public Publication Publication { get; set; } = null!;
}