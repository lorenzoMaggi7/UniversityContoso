using System.Text.Json.Serialization;

namespace UniversityContoso.Model
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudenteID { get; set; }
        public int CorsoID { get; set; }
        [JsonIgnore]
        public Studente? Studente { get; set; }
        [JsonIgnore]
        public Corso? Corso { get; set; }

    }
}
