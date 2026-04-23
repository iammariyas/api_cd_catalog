using MediatR; 
using CDCatalog.Application.DTOs;

namespace CDCatalog.Application.AddCD
{
    public class AddCDCommand : IRequest<CDDto>
    {
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public int Year { get; set; }
    }
}