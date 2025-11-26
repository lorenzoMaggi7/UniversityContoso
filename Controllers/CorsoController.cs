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

        // ---------------------------------------------------------------
        // GET: api/Corso
        // Restituisce la lista di tutti i corsi con il professore associato.
        // ---------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corso>>> GetAll()
        {
            return await _context.Corsi
                .Include(c => c.Professore)
                .AsNoTracking()
                .ToListAsync();
        }

        // ---------------------------------------------------------------
        // GET: api/Corso/{id}
        // Ritorna un singolo corso con il suo professore.
        // ---------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Corso>> GetById(int id)
        {
            var c = await _context.Corsi
                .Include(c => c.Professore)
                .FirstOrDefaultAsync(c => c.CorsoID == id);

            if (c == null)
                return NotFound();

            return c;
        }

        // ---------------------------------------------------------------
        // POST: api/Corso
        // Crea un nuovo corso (Crediti, ProfessoreID e Descrizione opzionali).
        // ---------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Corso>> Create(Corso c)
        {
            // Il campo Descrizione è opzionale, quindi non serve alcuna validazione speciale
            _context.Corsi.Add(c);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = c.CorsoID }, c);
        }

        // ---------------------------------------------------------------
        // PUT: api/Corso/{id}
        // Aggiorna un corso esistente, inclusa la nuova Descrizione.
        // ---------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Corso c)
        {
            if (id != c.CorsoID)
                return BadRequest();

            var corsoEsistente = await _context.Corsi.FindAsync(id);
            if (corsoEsistente == null)
                return NotFound();

            // Aggiorno manualmente solo i campi modificabili
            corsoEsistente.Titolo = c.Titolo;
            corsoEsistente.Crediti = c.Crediti;
            corsoEsistente.ProfessoriID = c.ProfessoriID;
            corsoEsistente.Descrizione = c.Descrizione; 

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ---------------------------------------------------------------
        // DELETE: api/Corso/{id}
        // Rimuove un corso.
        // ---------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Corsi.FindAsync(id);
            if (c == null)
                return NotFound();

            _context.Corsi.Remove(c);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
