using Direcional.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direcional.Persistence.Mapping;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
           .HasMaxLength(Usuario.PropertyLength.Email)
           .IsRequired();

        builder.Property(x => x.Senha)
           .HasMaxLength(Usuario.PropertyLength.Senha)
           .IsRequired();

        builder.Property(x => x.Nome)
           .HasMaxLength(Usuario.PropertyLength.Nome)
           .IsRequired();

        builder.Property(x => x.HashPassword)
           .HasMaxLength(Usuario.PropertyLength.HashPassword)
           .IsRequired();

        builder.Property(x => x.Salt)
           .HasMaxLength(Usuario.PropertyLength.Salt)
           .IsRequired();

        builder.Property(x => x.Tipo)
           .HasConversion<string>()
           .HasMaxLength(50)
           .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
