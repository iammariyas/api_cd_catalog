using MediatR;
using CDCatalog.Application.DTOs;

namespace CDCatalog.Application.Search;

public class SearchSongsQuery : IRequest<List<SongDto>>
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}
