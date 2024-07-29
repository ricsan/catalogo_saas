using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class ConfiguracaoDeContaConfiguration : IEntityTypeConfiguration<ConfiguracaoDeConta>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoDeConta> builder)
        {
            builder.ToTable("ConfiguracoesDeContas");

            builder.HasKey(r => new { r.Conta_Id, r.Parametro_Id });

            builder.Property(r => r.Valor)
                .HasMaxLength(250);
        }
    }
}
