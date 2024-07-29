using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(r => r.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Email)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(r => r.DataDeCadastro)
                .IsRequired();

            builder.Property(r => r.IdpIds)
                .HasMaxLength(300);
        }
    }
}
