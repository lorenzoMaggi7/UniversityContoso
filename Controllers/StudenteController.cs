using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudenteController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudenteController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studente>>> GetAll()
        {
            return await _context.Studenti.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Studente>> GetById(int id)
        {
            var stud = await _context.Studenti.FindAsync(id);
            if (stud == null) return NotFound();
            return stud;
        }

        [HttpPost]
        public async Task<ActionResult<Studente>> Create(Studente stud)
        {
            _context.Studenti.Add(stud);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stud.ID }, stud);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Studente stud)
        {
            if (id != stud.ID) return BadRequest();

            _context.Entry(stud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stud = await _context.Studenti.FindAsync(id);
            if (stud == null) return NotFound();

            _context.Studenti.Remove(stud);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
