namespace MD3.CatalogoSaaS.Model
{
    public class ContaDeSistema
    {
        public ContaDeSistema()
        {
            ConfiguracoesDaConta = new List<ConfiguracaoDeConta>();
        }
        public ContaDeSistema(Tenant tenant, Sistema sistema, PlanoDoSistema plano)
        {
            Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
            Tenant_Id = tenant.Id;

            Sistema = sistema ?? throw new ArgumentNullException(nameof(sistema));
            Sistema_Id = sistema.Id;

            Plano = plano ?? throw new ArgumentNullException(nameof(plano));
            Plano_Id = plano.Id;
        }

        /// <summary>
        /// Construtor com tipo primitivo é necessário para o funcionamento do Entity Framework!
        /// https://learn.microsoft.com/en-us/ef/core/modeling/constructors
        /// https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
        /// </summary>
        public ContaDeSistema(int? tenant_Id, int? sistema_Id, int? plano_Id)
        {
            if (tenant_Id is null || tenant_Id == 0)
                throw new ArgumentException(null, nameof(tenant_Id));

            if (sistema_Id is null || sistema_Id == 0)
                throw new ArgumentException(null, nameof(sistema_Id));

            if (plano_Id is null || plano_Id == 0)
                throw new ArgumentException(null, nameof(plano_Id));

            Tenant_Id = tenant_Id.Value;
            Sistema_Id = sistema_Id.Value;
            Plano_Id = plano_Id.Value;
        }


        public static ContaDeSistema New(Tenant tenant, Sistema sistema, ParametroDoSistema[] parametrosDoSistema, PlanoDoSistema planoDoSistema)
        {
            if (parametrosDoSistema == null)
                throw new ArgumentNullException(nameof(parametrosDoSistema));


            var conta = new ContaDeSistema(tenant, sistema, planoDoSistema)
            {
                Usuarios = new List<Usuario>(),
                ConfiguracoesDaConta = new List<ConfiguracaoDeConta>()
            };

            foreach (ParametroDoSistema parametro in parametrosDoSistema)
            {
                if (parametro.NivelDeConta)
                    conta.AdicionarConfiguracao(new ConfiguracaoDeConta(conta, parametro));
            }

            return conta;
        }
        public void LimparEntidadesDeRelacionamento()
        {
            if (Sistema_Id != null && Sistema_Id > 0)
                Sistema = null;

            if (Tenant_Id != null && Tenant_Id > 0)
                Tenant = null;

            if (Plano_Id != null && Plano_Id > 0)
                Plano = null;

            foreach (var item in ConfiguracoesDaContaFacade)
                item.LimparEntidadesDeRelacionamento();
        }


        public int? Id { get; set; }

        public int? Tenant_Id { get; set; }
        public virtual Tenant? Tenant { get; private set; }

        public int? Sistema_Id { get; set; }
        public virtual Sistema? Sistema { get; private set; }

        public int? Plano_Id { get; set; }
        public virtual PlanoDoSistema? Plano { get; private set; }


        public string GetIdSimbolico() => $"{Id:0000}-{Sistema_Id:000}";



        public virtual IReadOnlyCollection<Usuario>? Usuarios { get; private set; }
        private ICollection<Usuario>? UsuariosFacade
        {
            get => Usuarios as ICollection<Usuario>;
            set => Usuarios = value as IReadOnlyCollection<Usuario>;
        }
        public void AdicionarUsuario(Usuario usuario)
        {
            if (Usuarios == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Usuarios)}' na entidade '{nameof(ContaDeSistema)}' antes de continuar.");

            if (Usuarios.Any(r => r.GetHashCode() == usuario.GetHashCode()))
                throw new ArgumentException("Este usuário já existe na conta do sistema.");

            UsuariosFacade.Add(usuario);
        }
        public void RemoverUsuario(Usuario usuario)
        {
            if (Usuarios == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Usuarios)}' na entidade '{nameof(ContaDeSistema)}' antes de continuar.");

            if (!Usuarios.Any(r => r.GetHashCode() == usuario.GetHashCode()))
                throw new ArgumentException("Este usuário não existe na conta do sistema.");

            UsuariosFacade.Remove(usuario);
        }
        public bool ExisteUsuario(Usuario usuario)
        {
            if (Usuarios == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Usuarios)}' na entidade '{nameof(ContaDeSistema)}' antes de continuar.");

            return Usuarios.Any(r => r.GetHashCode() == usuario.GetHashCode());
        }


        public virtual IReadOnlyCollection<ConfiguracaoDeConta>? ConfiguracoesDaConta { get; private set; }
        private ICollection<ConfiguracaoDeConta>? ConfiguracoesDaContaFacade
        {
            get => ConfiguracoesDaConta as ICollection<ConfiguracaoDeConta>;
            set => ConfiguracoesDaConta = value as IReadOnlyCollection<ConfiguracaoDeConta>;
        }
        public void AdicionarConfiguracao(ConfiguracaoDeConta configuracao)
        {
            if (ConfiguracoesDaConta == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(ConfiguracoesDaConta)}' na entidade '{nameof(ContaDeSistema)}' antes de continuar.");

            if ((Id != null && (configuracao.Conta?.Id ?? configuracao.Conta_Id) != Id) || configuracao.Conta != this)
                throw new ArgumentException($"A configuração de '{configuracao.Parametro?.CodigoUnico}' foi criado para outra conta: '{configuracao.Conta?.Id ?? configuracao.Conta_Id}'.");

            if (ConfiguracoesDaConta.Any(r => r.GetHashCode() == configuracao.GetHashCode()))
                throw new ArgumentException("Esta configuração já existe na conta.");

            ConfiguracoesDaContaFacade.Add(configuracao);
        }
    }
}
