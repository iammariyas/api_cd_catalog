namespace CDCatalog.Application.DTOs;

public class SongDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string FirstName_Artist { get; set; } = "";
    public string LastName_Artist { get; set; } = "";
    public int Duration { get; set; }
}