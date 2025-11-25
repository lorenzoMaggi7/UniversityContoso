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

        // ===========================
        // GET: api/studente
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studente>>> GetStudenti()
        {
            return await _context.Studenti.ToListAsync();
        }

        // ===========================
        // GET: api/studente/5
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Studente>> GetStudente(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);

            if (studente == null)
                return NotFound();

            return studente;
        }

        // ===========================
        // POST: api/studente
        // ===========================
        [HttpPost]
        public async Task<ActionResult<Studente>> CreateStudente(Studente studente)
        {
            _context.Studenti.Add(studente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudente), new { id = studente.ID }, studente);
        }

        // ===========================
        // PUT: api/studente/5
        // ===========================
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

        // ===========================
        // DELETE: api/studente/5
        // ===========================
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

        // ===========================
        // POST: api/studente/login
        // ===========================
        [HttpPost("login")]
        public async Task<ActionResult<Studente>> Login([FromBody] LoginRequest login)
        {
            var studente = await _context.Studenti
                .FirstOrDefaultAsync(s => s.Nome == login.Nome && s.Password == login.Password);

            if (studente == null)
                return Unauthorized("Nome o password errati.");

            return studente;
        }
    }

    // DTO per login
    public class LoginRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
