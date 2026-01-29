using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.Paises.Queries;
using SmartCard.Application.Paises.Commands;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaisController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaisController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisDto>>> GetPais() => await _mediator.Send(new GetPaisesQuery());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaisDto>> GetPais(int id) {
        var res = await _mediator.Send(new GetPaisByIdQuery(id));
        return res == null ? NotFound() : res;
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostPais(CreatePaisCommand command) {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPais), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPais(int id, UpdatePaisCommand command) {
        if (id != command.IdPais) return BadRequest();
        if (!await _mediator.Send(command)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePais(int id) {
        if (!await _mediator.Send(new DeletePaisCommand(id))) return NotFound();
        return NoContent();
    }
}
