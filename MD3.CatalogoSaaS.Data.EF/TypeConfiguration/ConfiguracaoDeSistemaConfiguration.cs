using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class ConfiguracaoDeSistemaConfiguration : IEntityTypeConfiguration<ConfiguracaoDeSistema>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoDeSistema> builder)
        {
            builder.ToTable("ConfiguracoesDeSistemas");

            builder.HasKey(r => new { r.Sistema_Id, r.Parametro_Id });

            builder.Property(r => r.Valor)
                .HasMaxLength(250);
        }
    }
}
