using Microsoft.EntityFrameworkCore;
using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data
{
    public class CadCaminhoneiroContext : DbContext
    {
        public CadCaminhoneiroContext(DbContextOptions<CadCaminhoneiroContext> options) : base(options)
        {
        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Habilitacao> Habilitacao { get; set; }
        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<MarcaCaminhao> MarcaCaminhao { get; set; }
        public DbSet<ModeloCaminhao> ModeloCaminhao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadCaminhoneiroContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(p => p.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
