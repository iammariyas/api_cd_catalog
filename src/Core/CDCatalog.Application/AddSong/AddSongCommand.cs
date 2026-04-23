using MediatR;
using CDCatalog.Application.DTOs;

namespace CDCatalog.Application.AddSong;

public class AddSongCommand : IRequest<SongDto?>
{
    public int CDId { get; set; }
    public string Title { get; set; } = "";
    public string FirstName_Artist { get; set; } = "";
    public string LastName_Artist { get; set; } = "";
    public int Duration { get; set; }
}