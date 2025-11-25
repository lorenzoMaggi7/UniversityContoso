using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly UniversityContext _context;

        public EnrollmentController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
        {
            return await _context.Enrollments
                .Include(e => e.Studente)
                .Include(e => e.Corso)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetById(int id)
        {
            var e = await _context.Enrollments
                .Include(x => x.Studente)
                .Include(x => x.Corso)
                .FirstOrDefaultAsync(x => x.EnrollmentID == id);

            if (e == null) return NotFound();

            return e;
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> Create(Enrollment e)
        {
            _context.Enrollments.Add(e);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = e.EnrollmentID }, e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Enrollment e)
        {
            if (id != e.EnrollmentID) return BadRequest();

            _context.Entry(e).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Enrollments.FindAsync(id);
            if (e == null) return NotFound();

            _context.Enrollments.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
