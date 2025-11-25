namespace UniversityContoso.Model
{
    public class Professori
    {
        public int ProfessoriID { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DataAssunzione { get; set; }
    }
}
