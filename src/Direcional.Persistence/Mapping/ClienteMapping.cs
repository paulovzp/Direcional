using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
           .HasMaxLength(Cliente.PropertyLength.Email)
           .IsRequired();

        builder.Property(x => x.Telefone)
           .HasMaxLength(Cliente.PropertyLength.Telefone)
           .IsRequired();

        builder.Property(x => x.Nome)
           .HasMaxLength(Cliente.PropertyLength.Nome)
           .IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithOne(x => x.Cliente)
            .HasForeignKey<Cliente>(x => x.UsuarioId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
