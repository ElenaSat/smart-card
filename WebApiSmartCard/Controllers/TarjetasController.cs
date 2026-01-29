using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.Features.Tarjetas.Commands;
using SmartCard.Application.Features.Tarjetas.Queries;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarjetasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TarjetasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Tarjetas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarjetaDto>>> GetTarjetas()
    {
        return await _mediator.Send(new GetTarjetasQuery());
    }

    // GET: api/Tarjetas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TarjetaDto>> GetTarjeta(int id)
    {
        var tarjeta = await _mediator.Send(new GetTarjetaByIdQuery(id));
        if (tarjeta == null) return NotFound();
        return tarjeta;
    }

    // POST: api/Tarjetas
    [HttpPost]
    public async Task<ActionResult<int>> PostTarjeta(CreateTarjetaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTarjeta), new { id }, id);
    }

    // PUT: api/Tarjetas/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTarjeta(int id, UpdateTarjetaCommand command)
    {
        if (id != command.IdTarjeta) return BadRequest();
        
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        
        return NoContent();
    }

    // DELETE: api/Tarjetas/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTarjeta(int id)
    {
        var result = await _mediator.Send(new DeleteTarjetaCommand(id));
        if (!result) return NotFound();

        return NoContent();
    }
}
