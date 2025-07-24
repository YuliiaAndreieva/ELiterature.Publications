namespace Core.Dtos;

public class UpdatePublicationRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Type { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateOnly? PublicationYear { get; set; }
} 