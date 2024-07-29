using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class SistemaConfiguration : IEntityTypeConfiguration<Sistema>
    {
        public void Configure(EntityTypeBuilder<Sistema> builder)
        {
            builder.ToTable("Sistemas");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.CodigoInterno).IsUnique();

            builder
                .HasMany(r => r.Parametros)
                .WithOne(r => r.Sistema)
                .HasForeignKey(r => r.Sistema_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(r => r.Planos)
                .WithOne(r => r.Sistema)
                .HasForeignKey(r => r.Sistema_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(r => r.ConfiguracoesDoSistema)
                .WithOne(r => r.Sistema)
                .HasForeignKey(r => r.Sistema_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Rel
            builder
                .HasMany(r => r.Contas)
                .WithOne(r => r.Sistema)
                .HasForeignKey(r => r.Sistema_Id)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(r => r.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Descricao)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(r => r.CodigoInterno)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
