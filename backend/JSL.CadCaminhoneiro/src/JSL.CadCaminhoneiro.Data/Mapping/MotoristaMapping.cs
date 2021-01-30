using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motorista");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Cpf)
                .IsRequired()
                .HasColumnType("char(11)");

            builder.Property(p => p.DataNascimento)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.NomePai)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.NomeMae)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Naturalidade)
                .IsRequired()
                .HasColumnType("varchar(19)");

            builder.Property(p => p.NumeroRegistroGeral)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.OrgaoExpedicaoRegistroGeral)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.DataExpedicaoRegistroGeral)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataAlteracao)
                .HasColumnType("datetime");
                
        }
    }
}
