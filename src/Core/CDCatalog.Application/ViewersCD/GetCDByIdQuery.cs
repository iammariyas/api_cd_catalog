using MediatR;
using CDCatalog.Application.DTOs;

namespace CDCatalog.Application.ViewersCD;

public class GetCDByIdQuery : IRequest<CDDto?>
{
    public int Id { get; set; }
}
