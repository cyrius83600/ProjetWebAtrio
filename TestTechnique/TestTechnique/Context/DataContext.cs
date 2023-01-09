using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection.Emit;
using TestTechnique.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
