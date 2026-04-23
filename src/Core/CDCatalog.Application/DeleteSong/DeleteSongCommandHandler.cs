using MediatR;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.DeleteSong;

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, bool>
{
    private readonly ICDRepository _repo;

    public DeleteSongCommandHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        var cd = await _repo.GetCDByIdAsync(request.CDId);
        if (cd == null) return false;

        var song = cd.Songs.FirstOrDefault(s => s.Id == request.SongId);
        if (song == null) return false;

        cd.Songs.Remove(song);
        await _repo.DeleteSongAsync(request.SongId);
        
        return true;
    }
}
