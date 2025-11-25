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
        // Restituisce la lista completa di tutti i corsi presenti nel DB.
        // Usa AsNoTracking() perché qui non serve il tracking delle entità.
        // ---------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corso>>> GetAll()
        {
            return await _context.Corsi.AsNoTracking().ToListAsync();
        }

        // ---------------------------------------------------------------
        // GET: api/Corso/{id}
        // Cerca un corso tramite il suo ID.
        // Se non lo trova restituisce 404 (NotFound).
        // ---------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Corso>> GetById(int id)
        {
            var c = await _context.Corsi.FindAsync(id);
            if (c == null) return NotFound();
            return c;
        }

        // ---------------------------------------------------------------
        // POST: api/Corso
        // Crea un nuovo corso.
        // Dopo aver salvato nel DB, ritorna 201 (Created)
        // e include l’URL dell'oggetto appena creato.
        // ---------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Corso>> Create(Corso c)
        {
            _context.Corsi.Add(c);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = c.CorsoID }, c);
        }

        // ---------------------------------------------------------------
        // PUT: api/Corso/{id}
        // Aggiorna un corso esistente.
        // Verifica che l’ID nell’URL corrisponda a quello del body.
        // Se ok, EF Core aggiorna e salva.
        // Restituisce 204 (NoContent) perché non torna un oggetto.
        // ---------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Corso c)
        {
            if (id != c.CorsoID) return BadRequest();

            _context.Entry(c).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ---------------------------------------------------------------
        // DELETE: api/Corso/{id}
        // Elimina un corso tramite il suo ID.
        // Se il corso non esiste, restituisce 404.
        // Se esiste lo elimina e salva, restituendo 204 (NoContent).
        // ---------------------------------------------------------------
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
