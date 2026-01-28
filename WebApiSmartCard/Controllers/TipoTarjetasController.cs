using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiSmartCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTarjetasController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoTarjetasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TipoTarjetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTarjeta>>> GetTiposTarjeta()
        {
            return await _context.TiposTarjeta.ToListAsync();
        }

        // GET: api/TipoTarjetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTarjeta>> GetTipoTarjeta(int id)
        {
            var tipoTarjeta = await _context.TiposTarjeta.FindAsync(id);

            if (tipoTarjeta == null)
            {
                return NotFound();
            }

            return tipoTarjeta;
        }

        // PUT: api/TipoTarjetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTarjeta(int id, TipoTarjeta tipoTarjeta)
        {
            if (id != tipoTarjeta.IdTipo)
            {
                return BadRequest();
            }

            _context.Entry(tipoTarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTarjetaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoTarjetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoTarjeta>> PostTipoTarjeta(TipoTarjeta tipoTarjeta)
        {
            _context.TiposTarjeta.Add(tipoTarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTarjeta", new { id = tipoTarjeta.IdTipo }, tipoTarjeta);
        }

        // DELETE: api/TipoTarjetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTarjeta(int id)
        {
            var tipoTarjeta = await _context.TiposTarjeta.FindAsync(id);
            if (tipoTarjeta == null)
            {
                return NotFound();
            }

            _context.TiposTarjeta.Remove(tipoTarjeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoTarjetaExists(int id)
        {
            return _context.TiposTarjeta.Any(e => e.IdTipo == id);
        }
    }
}
