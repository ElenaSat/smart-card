using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.UsuarioSistemas.Queries;
using SmartCard.Application.UsuarioSistemas.Commands;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioSistemasController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsuarioSistemasController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioSistemaDto>>> Get() => await _mediator.Send(new GetUsuarioSistemasQuery());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioSistemaDto>> Get(int id) {
        var res = await _mediator.Send(new GetUsuarioSistemaByIdQuery(id));
        return res == null ? NotFound() : res;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(CreateUsuarioSistemaCommand command) {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateUsuarioSistemaCommand command) {
        if (id != command.IdUsuarioSistema) return BadRequest();
        if (!await _mediator.Send(command)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        if (!await _mediator.Send(new DeleteUsuarioSistemaCommand(id))) return NotFound();
        return NoContent();
    }
}
