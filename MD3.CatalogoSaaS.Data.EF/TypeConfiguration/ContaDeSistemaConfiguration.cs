using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class ContaDeSistemaConfiguration : IEntityTypeConfiguration<ContaDeSistema>
    {
        public void Configure(EntityTypeBuilder<ContaDeSistema> builder)
        {
            builder.ToTable("ContasDeSistema");

            builder.HasKey(r => r.Id);

            builder
                .HasMany(r => r.ConfiguracoesDaConta)
                .WithOne(r => r.Conta)
                .HasForeignKey(r => r.Conta_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(r => r.Usuarios)
                .WithMany(r => r.Contas)
                .UsingEntity<Dictionary<string, object>>(
                    "RelUsuarioContaDeSistema",
                    r => r.HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_RelUsuarioContaDeSistema_Usuarios_UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne<ContaDeSistema>()
                        .WithMany()
                        .HasForeignKey("ContaDeSistemaId")
                        .HasConstraintName("FK_RelUsuarioContaDeSistema_ContasDeSistema_ContaDeSistemaId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
