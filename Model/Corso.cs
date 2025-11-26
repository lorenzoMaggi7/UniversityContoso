using System.Text.Json.Serialization;

namespace UniversityContoso.Model
{
    public class Corso
    {
        public int CorsoID { get; set; }
        public string? Titolo { get; set; }
        public int? Crediti { get; set; }
        public string? Descrizione { get; set; }
        public int? PostiDisponibili { get; set; }
        public int? DurataMesi { get; set; }
        [JsonIgnore]
        public int? ProfessoriID { get; set; }
        [JsonIgnore] // Ignora la proprietà navigazionale in Swagger
        public Professori? Professore { get; set; }
        [JsonIgnore]
        public ICollection<Enrollment>? Enrollments { get; set; }

    }
}
