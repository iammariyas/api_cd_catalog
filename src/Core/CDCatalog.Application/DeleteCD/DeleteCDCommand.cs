using MediatR;

namespace CDCatalog.Application.DeleteCD;

public class DeleteCDCommand : IRequest<bool>
{
    public int Id { get; set; }
}
