using MediatR;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.Sort;

public class SortSongsCommandHandler : IRequestHandler<SortSongsCommand, bool>
{
    private readonly ICDRepository _repo;

    public SortSongsCommandHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(SortSongsCommand request, CancellationToken cancellationToken)
    {
        var cd = await _repo.GetCDByIdAsync(request.CDId);
        if (cd == null) return false;

        if (request.SortBy.ToLower() == "title")
        {
            cd.Songs = cd.Songs.OrderBy(s => s.Title).ToList();
        }
        else if (request.SortBy.ToLower() == "artist")
        {
            cd.Songs = cd.Songs.OrderBy(s => s.LastName_Artist).ThenBy(s => s.FirstName_Artist).ToList();
        }
        else
        {
            return false;
        }

        await _repo.UpdateCDAsync(cd);
        return true;
    }
}
