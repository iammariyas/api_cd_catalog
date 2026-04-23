using MediatR;

namespace CDCatalog.Application.Sort;

public class SortSongsCommand : IRequest<bool>
{
    public int CDId { get; set; }
    public string SortBy { get; set; } = ""; // "title" или "artist"
}
