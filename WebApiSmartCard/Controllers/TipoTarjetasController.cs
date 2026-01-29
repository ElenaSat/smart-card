using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.Features.TiposTarjeta.Queries;
using SmartCard.Application.Features.TiposTarjeta.Commands;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoTarjetasController : ControllerBase
{
    private readonly IMediator _mediator;
    public TipoTarjetasController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoTarjetaDto>>> Get() => await _mediator.Send(new GetTiposQuery());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoTarjetaDto>> Get(int id) {
        var res = await _mediator.Send(new GetTipoByIdQuery(id));
        return res == null ? NotFound() : res;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(CreateTipoCommand command) {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateTipoCommand command) {
        if (id != command.IdTipo) return BadRequest();
        if (!await _mediator.Send(command)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        if (!await _mediator.Send(new DeleteTipoCommand(id))) return NotFound();
        return NoContent();
    }
}
