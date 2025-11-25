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

        // ---------------------------------------------------------------
        // GET: api/professore
        // Ritorna tutti i professori
        // ---------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professori>>> GetAll()
        {
            return await _context.Professori.AsNoTracking().ToListAsync();
        }

        // ---------------------------------------------------------------
        // GET: api/professore/{id}
        // Ritorna un professore specifico
        // ---------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Professori>> GetById(int id)
        {
            var p = await _context.Professori.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        // ---------------------------------------------------------------
        // POST: api/professore
        // Crea un nuovo professore (Email e Password incluse)
        // ---------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Professori>> Create(Professori p)
        {
            _context.Professori.Add(p);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = p.ProfessoriID }, p);
        }

        // ---------------------------------------------------------------
        // PUT: api/professore/{id}
        // Aggiorna un professore
        // ---------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Professori p)
        {
            if (id != p.ProfessoriID) return BadRequest();

            _context.Entry(p).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------------------------------------------------------
        // DELETE: api/professore/{id}
        // Elimina un professore
        // ---------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Professori.FindAsync(id);
            if (p == null) return NotFound();

            _context.Professori.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------------------------------------------------------
        // POST: api/professore/login
        // Login tramite Email + Password
        // ---------------------------------------------------------------
        [HttpPost("login")]
        public async Task<ActionResult<Professori>> Login([FromBody] LoginRequest login)
        {
            var prof = await _context.Professori
                .FirstOrDefaultAsync(p => p.Email == login.Email && p.Password == login.Password);

            if (prof == null)
                return Unauthorized("Email o password errati.");

            return prof;
        }

        // ================================================================
        // POST: api/professore/IscrivitiCorso
        // Permette al professore di iscriversi a un corso
        // ================================================================
        [HttpPost("IscrivitiCorso")]
        public async Task<IActionResult> IscrivitiACorso([FromBody] IscrizioneCorsoRequest request)
        {
            // Controlla che il professore esista
            var prof = await _context.Professori.FindAsync(request.ProfessoreID);
            if (prof == null) return NotFound("Professore non trovato.");

            // Controlla che il corso esista
            var corso = await _context.Corsi.FindAsync(request.CorsoID);
            if (corso == null) return NotFound("Corso non trovato.");

            // Controlla che il corso non abbia già un professore
            if (corso.ProfessoriID != null)
                return BadRequest("Il corso ha già un professore assegnato.");

            // Assegna il professore al corso
            corso.ProfessoriID = prof.ProfessoriID;
            await _context.SaveChangesAsync();

            return Ok(corso);
        }
    }

    // DTO per il login
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // DTO per iscrizione corso
    public class IscrizioneCorsoRequest
    {
        public int ProfessoreID { get; set; }
        public int CorsoID { get; set; }
    }
}
