using MediatR;
using CDCatalog.Domain.Interfaces;

namespace CDCatalog.Application.DeleteCD;

public class DeleteCDCommandHandler : IRequestHandler<DeleteCDCommand, bool>
{
    private readonly ICDRepository _repo;

    public DeleteCDCommandHandler(ICDRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteCDCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteCDAsync(request.Id);
        return true;
    }
}
