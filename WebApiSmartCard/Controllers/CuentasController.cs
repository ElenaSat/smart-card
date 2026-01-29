using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.Cuentas.Commands;
using SmartCard.Application.Cuentas.Queries;
using SmartCard.Application.DTOs;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CuentasController : ControllerBase
{
    private readonly IMediator _mediator;

    public CuentasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Cuentas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CuentaDto>>> GetCuentas()
    {
        return await _mediator.Send(new GetCuentasQuery());
    }

    // GET: api/Cuentas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CuentaDto>> GetCuenta(int id)
    {
        var cuenta = await _mediator.Send(new GetCuentaByIdQuery(id));
        if (cuenta == null) return NotFound();
        return cuenta;
    }

    // POST: api/Cuentas
    [HttpPost]
    public async Task<ActionResult<int>> PostCuenta(CreateCuentaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCuenta), new { id }, id);
    }

    // PUT: api/Cuentas/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCuenta(int id, UpdateCuentaCommand command)
    {
        if (id != command.IdCuenta) return BadRequest();
        
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        
        return NoContent();
    }

    // DELETE: api/Cuentas/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCuenta(int id)
    {
        var result = await _mediator.Send(new DeleteCuentaCommand(id));
        if (!result) return NotFound();

        return NoContent();
    }
}
