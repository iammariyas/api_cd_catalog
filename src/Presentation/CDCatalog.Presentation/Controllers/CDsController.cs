using MediatR;
using Microsoft.AspNetCore.Mvc;
using CDCatalog.Application.AddCD;
using CDCatalog.Application.DeleteCD;
using CDCatalog.Application.ViewersCatalog;
using CDCatalog.Application.ViewersCD;
using CDCatalog.Application.AddSong;
using CDCatalog.Application.DeleteSong;
using CDCatalog.Application.Search;
using CDCatalog.Application.Sort;

namespace CDCatalog.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CDsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CDsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // 1. ДОБАВИТЬ ДИСК
    [HttpPost]
    public async Task<IActionResult> AddCD([FromBody] AddCDCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    // 2. УДАЛИТЬ ДИСК
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCD(int id)
    {
        var result = await _mediator.Send(new DeleteCDCommand { Id = id });
        return Ok(result);
    }

    // 3. ПОЛУЧИТЬ ВСЕ ДИСКИ
    [HttpGet]
    public async Task<IActionResult> GetAllCDs()
    {
        var result = await _mediator.Send(new GetAllCDsQuery());
        return Ok(result);
    }

    // 4. ПОЛУЧИТЬ ОДИН ДИСК
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCDById(int id)
    {
        var result = await _mediator.Send(new GetCDByIdQuery { Id = id });
        if (result == null) return NotFound("Диск не найден");
        return Ok(result);
    }

    // 5. ДОБАВИТЬ ПЕСНЮ
    [HttpPost("{cdId}/songs")]
    public async Task<IActionResult> AddSong(int cdId, [FromBody] AddSongCommand command)
    {
        command.CDId = cdId;
        var result = await _mediator.Send(command);
        if (result == null) return NotFound("Диск не найден");
        return Ok(result);
    }

    // 6. УДАЛИТЬ ПЕСНЮ
    [HttpDelete("{cdId}/songs/{songId}")]
    public async Task<IActionResult> DeleteSong(int cdId, int songId)
    {
        var result = await _mediator.Send(new DeleteSongCommand { CDId = cdId, SongId = songId });
        return Ok(result);
    }

    // 7. ПОИСК ПЕСЕН ПО ИСПОЛНИТЕЛЮ
    [HttpGet("search")]
    public async Task<IActionResult> SearchSongs([FromQuery] string firstName, [FromQuery] string lastName)
    {
        var result = await _mediator.Send(new SearchSongsQuery { FirstName = firstName, LastName = lastName });
        return Ok(result);
    }

    // 8. СОРТИРОВКА ПЕСЕН
    [HttpPut("{cdId}/songs/sort")]
    public async Task<IActionResult> SortSongs(int cdId, [FromQuery] string sortBy)
    {
        var result = await _mediator.Send(new SortSongsCommand { CDId = cdId, SortBy = sortBy });
        return Ok(result);
    }
}
