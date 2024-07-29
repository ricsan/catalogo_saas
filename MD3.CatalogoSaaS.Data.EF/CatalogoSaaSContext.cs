using MD3.CatalogoSaaS.Data.EF.TypeConfiguration;
using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Data.EF
{
    public class CatalogoSaaSContext : DbContext
    {
        public static string? connectionStringDefault;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<PlanoDoSistema> PlanosDeSistema { get; set; }
        public DbSet<ParametroDoSistema> ParametrosDeSistema { get; set; }
        public DbSet<ConfiguracaoDeSistema> ConfiguracoesDeSistema { get; set; }

        public DbSet<ContaDeSistema> ContasDeSistema { get; set; }
        public DbSet<ConfiguracaoDeConta> ConfiguracoesDeContasDeSistema { get; set; }



        public CatalogoSaaSContext()
            : base(new DbContextOptionsBuilder().UseSqlServer(connectionStringDefault).Options)
        {

        }

        public CatalogoSaaSContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UsuarioConfiguration().Configure(modelBuilder.Entity<Usuario>());
            new TenantConfiguration().Configure(modelBuilder.Entity<Tenant>());

            new ContaDeSistemaConfiguration().Configure(modelBuilder.Entity<ContaDeSistema>());
            new ConfiguracaoDeContaConfiguration().Configure(modelBuilder.Entity<ConfiguracaoDeConta>());

            new SistemaConfiguration().Configure(modelBuilder.Entity<Sistema>());
            new PlanoDoSistemaConfiguration().Configure(modelBuilder.Entity<PlanoDoSistema>());
            new ParametroDoSistemaConfiguration().Configure(modelBuilder.Entity<ParametroDoSistema>());
            new ConfiguracaoDeSistemaConfiguration().Configure(modelBuilder.Entity<ConfiguracaoDeSistema>());

            base.OnModelCreating(modelBuilder);
        }
    }
}