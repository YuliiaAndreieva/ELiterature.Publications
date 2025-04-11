namespace Core.Dtos.Writers;

public class WriterDto
{
    public long Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}