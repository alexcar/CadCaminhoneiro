using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(p => p.Complemento)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Bairro)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Municipio)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Uf)
                .IsRequired()
                .HasColumnType("char(2)");

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasColumnType("char(8)");

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataAlteracao)
                .HasColumnType("datetime");
        }
    }
}
