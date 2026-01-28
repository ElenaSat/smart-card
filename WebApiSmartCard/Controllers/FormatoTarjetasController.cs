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
    public class FormatoTarjetasController : ControllerBase
    {
        private readonly DataContext _context;

        public FormatoTarjetasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FormatoTarjetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormatoTarjeta>>> GetFormatosTarjeta()
        {
            return await _context.FormatosTarjeta.ToListAsync();
        }

        // GET: api/FormatoTarjetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormatoTarjeta>> GetFormatoTarjeta(int id)
        {
            var formatoTarjeta = await _context.FormatosTarjeta.FindAsync(id);

            if (formatoTarjeta == null)
            {
                return NotFound();
            }

            return formatoTarjeta;
        }

        // PUT: api/FormatoTarjetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormatoTarjeta(int id, FormatoTarjeta formatoTarjeta)
        {
            if (id != formatoTarjeta.IdFormato)
            {
                return BadRequest();
            }

            _context.Entry(formatoTarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormatoTarjetaExists(id))
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

        // POST: api/FormatoTarjetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormatoTarjeta>> PostFormatoTarjeta(FormatoTarjeta formatoTarjeta)
        {
            _context.FormatosTarjeta.Add(formatoTarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormatoTarjeta", new { id = formatoTarjeta.IdFormato }, formatoTarjeta);
        }

        // DELETE: api/FormatoTarjetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormatoTarjeta(int id)
        {
            var formatoTarjeta = await _context.FormatosTarjeta.FindAsync(id);
            if (formatoTarjeta == null)
            {
                return NotFound();
            }

            _context.FormatosTarjeta.Remove(formatoTarjeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormatoTarjetaExists(int id)
        {
            return _context.FormatosTarjeta.Any(e => e.IdFormato == id);
        }
    }
}
