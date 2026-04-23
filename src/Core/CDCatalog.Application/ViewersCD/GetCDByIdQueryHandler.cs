using MediatR;
using CDCatalog.Application.DTOs;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.ViewersCD;

public class GetCDByIdQueryHandler : IRequestHandler<GetCDByIdQuery, CDDto?>
{
    private readonly ICDRepository _repo;

    public GetCDByIdQueryHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<CDDto?> Handle(GetCDByIdQuery request, CancellationToken cancellationToken)
    {
        var cd = await _repo.GetCDByIdAsync(request.Id);
        if (cd == null) return null;
        
        return new CDDto
        {
            Id = cd.Id,
            Title = cd.Title,
            Artist = cd.Artist,
            Year = cd.Year
        };
    }
}