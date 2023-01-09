using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TestTechnique.Entities;

namespace TestTechnique.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Emploi> Emplois { get; set; }
        public DbSet<Personne> Personnes { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
