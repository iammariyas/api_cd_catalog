using MediatR;
using CDCatalog.Application.DTOs;
using CDCatalog.Domain.Entities;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.AddCD;

public class AddCDCommandHandler : IRequestHandler<AddCDCommand, CDDto>
{
    private readonly ICDRepository _repo;

    public AddCDCommandHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<CDDto> Handle(AddCDCommand request, CancellationToken cancellationToken)
    {
        var cd = new CD
        {
            Title = request.Title,
            Artist = request.Artist,
            Year = request.Year,
            Songs = new List<Song>()
        };

        await _repo.AddCDAsync(cd);

        return new CDDto
        {
            Id = cd.Id,
            Title = cd.Title,
            Artist = cd.Artist,
            Year = cd.Year
        };
    }
}