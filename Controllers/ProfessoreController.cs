using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoreController : ControllerBase
    {
        private readonly UniversityContext _context;

        public ProfessoreController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professori>>> GetAll()
        {
            return await _context.Professori.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professori>> GetById(int id)
        {
            var p = await _context.Professori.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public async Task<ActionResult<Professori>> Create(Professori p)
        {
            _context.Professori.Add(p);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = p.ProfessoriID }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Professori p)
        {
            if (id != p.ProfessoriID) return BadRequest();

            _context.Entry(p).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Professori.FindAsync(id);
            if (p == null) return NotFound();

            _context.Professori.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
