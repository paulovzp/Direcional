using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("Vendedores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
           .HasMaxLength(Vendedor.PropertyLength.Email)
           .IsRequired();

        builder.Property(x => x.Telefone)
           .HasMaxLength(Vendedor.PropertyLength.Telefone)
           .IsRequired();

        builder.Property(x => x.Nome)
           .HasMaxLength(Vendedor.PropertyLength.Nome)
           .IsRequired();

        builder.Property(x => x.DataFim)
           .IsRequired(false);

        builder.Property(x => x.DataInicio)
           .IsRequired();

        builder.Property(x => x.Codigo)
           .HasMaxLength(Vendedor.PropertyLength.Codigo)
           .IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithOne(x => x.Vendedor)
            .HasForeignKey<Vendedor>(x => x.UsuarioId);


        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
