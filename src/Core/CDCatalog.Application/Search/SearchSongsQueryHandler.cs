using MediatR;
using CDCatalog.Application.DTOs;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.Search;

public class SearchSongsQueryHandler : IRequestHandler<SearchSongsQuery, List<SongDto>>
{
    private readonly ICDRepository _repo;

    public SearchSongsQueryHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SongDto>> Handle(SearchSongsQuery request, CancellationToken cancellationToken)
    {
        var songs = await _repo.GetSongsByArtistAsync(request.FirstName, request.LastName);
        
        return songs.Select(s => new SongDto
        {
            Id = s.Id,
            Title = s.Title,
            FirstName_Artist = s.FirstName_Artist,
            LastName_Artist = s.LastName_Artist,
            Duration = s.Duration
        }).ToList();
    }
}
