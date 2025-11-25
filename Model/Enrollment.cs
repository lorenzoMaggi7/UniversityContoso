namespace UniversityContoso.Model
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudenteID { get; set; }
        public int CorsoID { get; set; }
        public Studente? Studente { get; set; }
        public Corso? Corso { get; set; }

    }
}
