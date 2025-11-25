using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorsoController : ControllerBase
    {
        private readonly UniversityContext _context;

        public CorsoController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corso>>> GetAll()
        {
            return await _context.Corsi.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Corso>> GetById(int id)
        {
            var c = await _context.Corsi.FindAsync(id);
            if (c == null) return NotFound();
            return c;
        }

        [HttpPost]
        public async Task<ActionResult<Corso>> Create(Corso c)
        {
            _context.Corsi.Add(c);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = c.CorsoID }, c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Corso c)
        {
            if (id != c.CorsoID) return BadRequest();

            _context.Entry(c).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Corsi.FindAsync(id);
            if (c == null) return NotFound();

            _context.Corsi.Remove(c);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
