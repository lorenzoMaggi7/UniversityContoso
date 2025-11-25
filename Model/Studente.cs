namespace UniversityContoso.Model
{
    public class Studente
    {

        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public DateTime DataIscrizione { get; set; }
        public string? Password { get; set; } = string.Empty;
        public ICollection<Enrollment>? Enrollments { get; set; }


    }
}
