using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JSL.CadCaminhoneiro.Data.Mapping
{
    public class ModeloCaminhaoMapping : IEntityTypeConfiguration<ModeloCaminhao>
    {
        public void Configure(EntityTypeBuilder<ModeloCaminhao> builder)
        {
            builder.ToTable("ModeloCaminhao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Ano)
                .IsRequired()
                .HasColumnType("varchar(4)");

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataAlteracao)
                .HasColumnType("datetime");
        }
    }
}
