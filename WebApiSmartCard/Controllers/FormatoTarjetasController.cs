using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.Formatos.Queries;
using SmartCard.Application.Formatos.Commands;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatoTarjetasController : ControllerBase
{
    private readonly IMediator _mediator;
    public FormatoTarjetasController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormatoTarjetaDto>>> Get() => await _mediator.Send(new GetFormatosQuery());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FormatoTarjetaDto>> Get(int id) {
        var res = await _mediator.Send(new GetFormatoByIdQuery(id));
        return res == null ? NotFound() : res;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(CreateFormatoCommand command) {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateFormatoCommand command) {
        if (id != command.IdFormato) return BadRequest();
        if (!await _mediator.Send(command)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        if (!await _mediator.Send(new DeleteFormatoCommand(id))) return NotFound();
        return NoContent();
    }
}
