using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class ReservaMapping : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataReserva)
           .IsRequired();

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Reservas)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Apartamento)
            .WithMany(x => x.Reservas)
            .HasForeignKey(x => x.ApartamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Vendedor)
            .WithMany(x => x.Reservas)
            .HasForeignKey(x => x.VendedorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}