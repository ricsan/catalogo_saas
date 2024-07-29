using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class ParametroDoSistemaConfiguration : IEntityTypeConfiguration<ParametroDoSistema>
    {
        public void Configure(EntityTypeBuilder<ParametroDoSistema> builder)
        {
            builder.ToTable("ParametrosDosSistemas");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => new { p.Sistema_Id, p.CodigoUnico }).IsUnique();

            // Rel
            builder
                .HasMany(r => r.ConfiguracoesNasContas)
                .WithOne(r => r.Parametro)
                .HasForeignKey(r => r.Parametro_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Rel
            builder
                .HasMany(r => r.ConfiguracoesNosSistemas)
                .WithOne(r => r.Parametro)
                .HasForeignKey(r => r.Parametro_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Id)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(r => r.NivelDeConta)
                .IsRequired();
        }
    }
}
