using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCard.Application.DTOs;
using SmartCard.Application.Features.Usuarios.Queries;
using SmartCard.Application.Features.Usuarios.Commands;

namespace WebApiSmartCard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
    {
        return await _mediator.Send(new GetUsuariosQuery());
    }

    // GET: api/Usuarios/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
    {
        var usuario = await _mediator.Send(new GetUsuarioByIdQuery(id));
        if (usuario == null) return NotFound();
        return usuario;
    }

    // POST: api/Usuarios
    [HttpPost]
    public async Task<ActionResult<int>> PostUsuario(CreateUsuarioCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUsuario), new { id }, id);
    }

    // PUT: api/Usuarios/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUsuario(int id, UpdateUsuarioCommand command)
    {
        if (id != command.IdUsuario) return BadRequest();
        
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        
        return NoContent();
    }

    // DELETE: api/Usuarios/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var result = await _mediator.Send(new DeleteUsuarioCommand(id));
        if (!result) return NotFound();

        return NoContent();
    }
}
