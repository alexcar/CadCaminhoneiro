using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class CaminhaoMapping : IEntityTypeConfiguration<Caminhao>
    {
        public void Configure(EntityTypeBuilder<Caminhao> builder)
        {
            builder.ToTable("Caminhao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Placa)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(p => p.Eixo)
                .IsRequired()
                .HasColumnType("tinyint");

            builder.Property(p => p.Observacao)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataAlteracao)
                .HasColumnType("datetime");
        }
    }
}
