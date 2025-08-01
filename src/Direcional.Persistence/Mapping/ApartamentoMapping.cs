using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class ApartamentoMapping : IEntityTypeConfiguration<Apartamento>
{
    public void Configure(EntityTypeBuilder<Apartamento> builder)
    {
        builder.ToTable("Apartamentos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
           .HasMaxLength(Apartamento.PropertyLength.Nome)
           .IsRequired();

        builder.Property(x => x.Numero)
           .IsRequired();

        builder.Property(x => x.Andar)
           .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}