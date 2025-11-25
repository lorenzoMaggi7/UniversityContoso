namespace UniversityContoso.Model
{
    public class Corso
    {
        public int CorsoID { get; set; }
        public string? Titolo { get; set; }
        public int Crediti { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

    }
}
