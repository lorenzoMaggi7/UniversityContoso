using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;
using UniversityContoso.Model;

namespace UniversityContoso.Controllers
{
    // Indica che il controller risponde come API REST
    [ApiController]

    // Imposta il percorso base dell'API:
    // es: api/Professore
    [Route("api/[controller]")]
    public class ProfessoreController : ControllerBase
    {
        // Il DbContext che permette di accedere al database
        private readonly UniversityContext _context;

        // Il costruttore riceve il contesto tramite Dependency Injection
        public ProfessoreController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Professore
        // ➜ Ritorna la lista di tutti i professori
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professori>>> GetAll()
        {
            // AsNoTracking() migliora le performance per letture senza tracking
            return await _context.Professori.AsNoTracking().ToListAsync();
        }

        // GET: api/Professore/5
        // ➜ Ritorna un singolo professore cercato tramite ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Professori>> GetById(int id)
        {
            var p = await _context.Professori.FindAsync(id);

            // Se non trovato → HTTP 404
            if (p == null) return NotFound();

            return p;
        }

        // POST: api/Professore
        // ➜ Crea un nuovo professore nel database
        [HttpPost]
        public async Task<ActionResult<Professori>> Create(Professori p)
        {
            _context.Professori.Add(p);   // Aggiunge l’oggetto al DbSet
            await _context.SaveChangesAsync();  // Salva le modifiche

            // Ritorna HTTP 201 + link alla nuova risorsa creata
            return CreatedAtAction(nameof(GetById), new { id = p.ProfessoriID }, p);
        }

        // PUT: api/Professore/5
        // ➜ Aggiorna i dati di un professore esistente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Professori p)
        {
            // Controllo di coerenza: l’ID nel body deve combaciare con l’URL
            if (id != p.ProfessoriID) return BadRequest();

            // Notifica a EF Core che l'entità va aggiornata
            _context.Entry(p).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Nessun contenuto da ritornare → HTTP 204
            return NoContent();
        }

        // DELETE: api/Professore/5
        // ➜ Elimina un professore tramite ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Professori.FindAsync(id);

            // Se non esiste → HTTP 404
            if (p == null) return NotFound();

            _context.Professori.Remove(p); // Rimuove dal database
            await _context.SaveChangesAsync();

            // Ritorna HTTP 204 (successo senza contenuto)
            return NoContent();
        }
    }
}
