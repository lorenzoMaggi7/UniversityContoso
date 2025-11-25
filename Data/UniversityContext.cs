using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniversityContoso.Model;

namespace UniversityContoso.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Studente> Studenti { get; set; }
        public DbSet<Professori> Professori { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
