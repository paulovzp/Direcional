using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Direcional.Persistence;

public class VendaMapping : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataVenda)
           .IsRequired();

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Vendas)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Apartamento)
            .WithMany(x => x.Vendas)
            .HasForeignKey(x => x.ApartamentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Vendedor)
            .WithMany(x => x.Vendas)
            .HasForeignKey(x => x.VendedorId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}