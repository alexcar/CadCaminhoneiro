using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class HabilitacaoMapping : IEntityTypeConfiguration<Habilitacao>
    {
        public void Configure(EntityTypeBuilder<Habilitacao> builder)
        {
            builder.ToTable("Habilitacao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroRegistro)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Categoria)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(p => p.DataPrimeiraHabilitacao)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.DataValidade)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.DataEmissao)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataAlteracao)
                .HasColumnType("datetime");
        }
    }
}
