using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly UniversityContext _context;

        public EnrollmentController(UniversityContext context)
        {
            _context = context;
        }

        // ===========================
        // GET: api/enrollment
        // Lista tutti gli enrollment
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
        {
            return await _context.Enrollments
                .Include(e => e.Studente)
                .Include(e => e.Corso)
                .AsNoTracking()
                .ToListAsync();
        }

        // ===========================
        // GET: api/enrollment/{id}
        // Ritorna un enrollment specifico
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetById(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Studente)
                .Include(e => e.Corso)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null) return NotFound();
            return enrollment;
        }

        // ===========================
        // POST: api/enrollment
        // Iscrive uno studente a un corso esistente
        // ===========================
        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateEnrollment(Enrollment enrollment)
        {
            // Controlla studente
            var studente = await _context.Studenti.FindAsync(enrollment.StudenteID);
            if (studente == null) return BadRequest("Studente inesistente.");

            // Controlla corso
            var corso = await _context.Corsi.FindAsync(enrollment.CorsoID);
            if (corso == null) return BadRequest("Corso inesistente.");

            // Controlla che lo studente non sia già iscritto
            var esistente = await _context.Enrollments
                .AnyAsync(e => e.StudenteID == enrollment.StudenteID && e.CorsoID == enrollment.CorsoID);
            if (esistente) return BadRequest("Studente già iscritto a questo corso.");

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = enrollment.EnrollmentID }, enrollment);
        }

        // ===========================
        // DELETE: api/enrollment/{id}
        // Rimuove un enrollment
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
