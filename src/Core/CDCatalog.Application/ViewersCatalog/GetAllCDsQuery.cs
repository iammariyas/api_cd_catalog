using MediatR;
using CDCatalog.Application.DTOs;

namespace CDCatalog.Application.ViewersCatalog;

public class GetAllCDsQuery : IRequest<List<CDDto>>
{
}
