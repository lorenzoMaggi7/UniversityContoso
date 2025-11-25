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

        // ----------------------------------------------------------
        // GET: api/Enrollment
        // Restituisce TUTTI gli Enrollment presenti nel database.
        // Include anche i dati correlati di Studente e Corso.
        // ----------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
        {
            return await _context.Enrollments
                .Include(e => e.Studente) // Include i dati dello studente
                .Include(e => e.Corso)    // Include i dati del corso
                .AsNoTracking()           // Query più performante in sola lettura
                .ToListAsync();
        }

        // ----------------------------------------------------------
        // GET: api/Enrollment/{id}
        // Restituisce un singolo Enrollment cercato per ID.
        // Se non esiste restituisce 404 NotFound.
        // Include anche i dati correlati di Studente e Corso.
        // ----------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetById(int id)
        {
            var e = await _context.Enrollments
                .Include(x => x.Studente)
                .Include(x => x.Corso)
                .FirstOrDefaultAsync(x => x.EnrollmentID == id);

            if (e == null) return NotFound(); // Nessun enrollment trovato

            return e;
        }

        // ----------------------------------------------------------
        // POST: api/Enrollment
        // Crea un nuovo Enrollment nel database.
        // Restituisce 201 Created e l’oggetto appena inserito.
        // ----------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Enrollment>> Create(Enrollment e)
        {
            _context.Enrollments.Add(e);  // Aggiunge l’oggetto al tracking EF
            await _context.SaveChangesAsync(); // Salva nel database

            return CreatedAtAction(nameof(GetById),
                new { id = e.EnrollmentID }, e);
        }

        // ----------------------------------------------------------
        // PUT: api/Enrollment/{id}
        // Aggiorna un Enrollment esistente.
        // Se l’ID passato NON corrisponde, restituisce 400 BadRequest.
        // Restituisce 204 NoContent se l’update va a buon fine.
        // ----------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Enrollment e)
        {
            if (id != e.EnrollmentID) return BadRequest();

            _context.Entry(e).State = EntityState.Modified; // Segna come modificato
            await _context.SaveChangesAsync();

            return NoContent(); // Aggiornamento riuscito
        }

        // ----------------------------------------------------------
        // DELETE: api/Enrollment/{id}
        // Elimina un Enrollment dal database.
        // Se non esiste restituisce 404.
        // Se eliminato correttamente, restituisce 204 NoContent.
        // ----------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Enrollments.FindAsync(id);

            if (e == null) return NotFound(); // Non trovato

            _context.Enrollments.Remove(e); // Elimino l’entità
            await _context.SaveChangesAsync();

            return NoContent(); // Eliminazione avvenuta
        }
    }
}
