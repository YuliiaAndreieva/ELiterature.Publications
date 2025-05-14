using Data.Entities.Enums;

namespace Data.Entities;

public class Publication
{
    public long Id { get; set; } 
    
    public required string Title { get; set; } 
    
    public required string Description { get; set; } 
    
    public DateOnly? PublicationYear { get; set; } 
    
    public PublicationType Type { get; set; } 
    
    public required string Text { get; set; }

    public IEnumerable<Author> Authors { get; set; } = new List<Author>();

    public IEnumerable<LiteratureDirection> LiteratureDirection { get; set; } = new List<LiteratureDirection>();

    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();

    public IEnumerable<PublicationPhoto> Photos { get; set; } = new List<PublicationPhoto>();
}