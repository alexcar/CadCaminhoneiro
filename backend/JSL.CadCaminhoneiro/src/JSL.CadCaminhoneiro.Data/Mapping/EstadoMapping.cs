using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");

            builder.HasKey(p => p.Uf);

            builder.Property(p => p.Uf)
                .HasColumnType("char(2)");

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(20)");
        }
    }
}
