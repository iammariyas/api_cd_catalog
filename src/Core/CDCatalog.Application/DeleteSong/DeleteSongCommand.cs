using MediatR;

namespace CDCatalog.Application.DeleteSong;

public class DeleteSongCommand : IRequest<bool>
{
    public int CDId { get; set; }
    public int SongId { get; set; }
}
