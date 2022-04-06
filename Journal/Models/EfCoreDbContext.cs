using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Models
{
    public class EfCoreDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=WIN-7HQC4KAK832;Initial Catalog=JournalDb;Integrated Security=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                .HasOne<Subject>(s => s.Subject)
                .WithMany(p => p.Points)
                .HasForeignKey(s => s.SubjectId);
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Point> Points { get; set; }
    }
}
