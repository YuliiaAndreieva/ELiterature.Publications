namespace Core.Dtos.LiteratureDirection;

public class LiteratureDirectionCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int StartCentury { get; set; }
    public int EndCentury { get; set; }
}