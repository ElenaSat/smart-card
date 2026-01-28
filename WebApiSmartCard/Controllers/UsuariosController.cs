using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApiSmartCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Cuentas)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
                return NotFound();

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.UtcNow;
            usuario.UsuarioCreacion = 1; // Id usuario_sistema actual (cámbialo según tu auth)

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest();

            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null)
                return NotFound();

            // Actualiza campos necesarios
            existente.Titulo = usuario.Titulo;
            existente.Nombre = usuario.Nombre;
            existente.Apellido = usuario.Apellido;
            existente.InfoExtra = usuario.InfoExtra;
            existente.FechaModificacion = DateTime.UtcNow;
            existente.UsuarioModificacion = 1; // usuario_sistema actual

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
