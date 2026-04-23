using MediatR;
using CDCatalog.Application.DTOs;
using CDCatalog.Domain.Entities;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.AddSong;

public class AddSongCommandHandler : IRequestHandler<AddSongCommand, SongDto?>
{
    private readonly ICDRepository _repo;

    public AddSongCommandHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<SongDto?> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        var cd = await _repo.GetCDByIdAsync(request.CDId);
        if (cd == null) return null;

        var song = new Song
        {
            Title = request.Title,
            FirstName_Artist = request.FirstName_Artist,
            LastName_Artist = request.LastName_Artist,
            Duration = request.Duration,
            CDId = request.CDId
        };

        await _repo.AddSongAsync(song);
        cd.Songs.Add(song);

        return new SongDto
        {
            Id = song.Id,
            Title = song.Title,
            FirstName_Artist = song.FirstName_Artist,
            LastName_Artist = song.LastName_Artist,
            Duration = song.Duration
        };
    }
}
