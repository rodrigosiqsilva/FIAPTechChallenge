using Microsoft.EntityFrameworkCore;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository.Configuration;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;      
        }

        public ApplicationDbContext()
        {
                
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Promocao> Promocao { get; set; }
        public DbSet<Jogo> Jogo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
