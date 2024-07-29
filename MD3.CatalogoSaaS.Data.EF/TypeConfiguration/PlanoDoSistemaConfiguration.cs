using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class PlanoDoSistemaConfiguration : IEntityTypeConfiguration<PlanoDoSistema>
    {
        public void Configure(EntityTypeBuilder<PlanoDoSistema> builder)
        {
            builder.ToTable("PlanosDosSistemas");

            builder.HasKey(p => p.Id);

            // Rel
            builder
                .HasMany(r => r.Contas)
                .WithOne(r => r.Plano)
                .HasForeignKey(r => r.Plano_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Descricao)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(r => r.Ativo)
                .IsRequired();
        }
    }
}
