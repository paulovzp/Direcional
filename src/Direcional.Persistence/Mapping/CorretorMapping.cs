using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class CorretorMapping : IEntityTypeConfiguration<Corretor>
{
    public void Configure(EntityTypeBuilder<Corretor> builder)
    {
        builder.ToTable("Corretores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
           .HasMaxLength(Corretor.PropertyLength.Email)
           .IsRequired();

        builder.Property(x => x.Telefone)
           .HasMaxLength(Corretor.PropertyLength.Telefone)
           .IsRequired();

        builder.Property(x => x.Nome)
           .HasMaxLength(Corretor.PropertyLength.Nome)
           .IsRequired();

        builder.Property(x => x.DataFim)
           .IsRequired(false);

        builder.Property(x => x.DataInicio)
           .IsRequired();

        builder.Property(x => x.Codigo)
           .HasMaxLength(Corretor.PropertyLength.Codigo)
           .IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithOne(x => x.Corretor)
            .HasForeignKey<Corretor>(x => x.UsuarioId);


        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
