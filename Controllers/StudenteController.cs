using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudenteController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudenteController(UniversityContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET: api/studente
        // Ritorna la lista di tutti gli studenti
        // =====================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studente>>> GetStudenti()
        {
            return await _context.Studenti.ToListAsync();
        }

        // =====================================================
        // GET: api/studente/{id}
        // Ritorna uno studente specifico tramite ID
        // =====================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Studente>> GetStudente(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);

            if (studente == null)
                return NotFound();

            return studente;
        }

        // =====================================================
        // POST: api/studente
        // Crea un nuovo studente (Email e Password incluse)
        // =====================================================
        [HttpPost]
        public async Task<ActionResult<Studente>> CreateStudente(Studente studente)
        {
            _context.Studenti.Add(studente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudente), new { id = studente.ID }, studente);
        }

        // =====================================================
        // PUT: api/studente/{id}
        // Aggiorna uno studente esistente (incluso Email e Password)
        // =====================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudente(int id, Studente studente)
        {
            if (id != studente.ID)
                return BadRequest();

            _context.Entry(studente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Studenti.Any(e => e.ID == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // =====================================================
        // DELETE: api/studente/{id}
        // Cancella uno studente
        // =====================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudente(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);
            if (studente == null)
                return NotFound();

            _context.Studenti.Remove(studente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =====================================================
        // POST: api/studente/login
        // Login tramite Email + Password
        // =====================================================
        [HttpPost("login")]
        public async Task<ActionResult<Studente>> Login([FromBody] LoginRequest login)
        {
            var studente = await _context.Studenti
                .FirstOrDefaultAsync(s => s.Email == login.Email && s.Password == login.Password);

            if (studente == null)
                return Unauthorized("Email o password errati.");

            return studente;
        }
    }

    // DTO per la richiesta di login
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
