using MediatR;
using CDCatalog.Application.DTOs;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.ViewersCatalog;

public class GetAllCDsQueryHandler : IRequestHandler<GetAllCDsQuery, List<CDDto>>
{
    private readonly ICDRepository _repo;

    public GetAllCDsQueryHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CDDto>> Handle(GetAllCDsQuery request, CancellationToken cancellationToken)
    {
        var cds = await _repo.GetAllAsync();
        
        return cds.Select(c => new CDDto
        {
            Id = c.Id,
            Title = c.Title,
            Artist = c.Artist,
            Year = c.Year
        }).ToList();
    }
}
