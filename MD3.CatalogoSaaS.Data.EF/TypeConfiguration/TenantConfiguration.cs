using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MD3.CatalogoSaaS.Data.EF.TypeConfiguration
{
    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(x => x.Id);

            builder
                .HasMany(r => r.Contas)
                .WithOne(r => r.Tenant)
                .HasForeignKey(r => r.Tenant_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Nome)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
