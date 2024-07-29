namespace MD3.CatalogoSaaS.Model
{
    public class Sistema
    {
        public Sistema()
        {

        }
        public Sistema(string codigoInterno, string nome, string descricao)
        {
            if (string.IsNullOrWhiteSpace(codigoInterno))
                throw new ArgumentNullException(nameof(codigoInterno));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentNullException(nameof(descricao));

            CodigoInterno = codigoInterno;
            Nome = nome;
            Descricao = descricao;
        }


        public static Sistema New(string codigoInterno, string nome, string descricao)
        {
            var sistema = new Sistema(codigoInterno, nome, descricao)
            {
                Planos = new List<PlanoDoSistema>(),
                Parametros = new List<ParametroDoSistema>(),
                ConfiguracoesDoSistema = new List<ConfiguracaoDeSistema>()
            };

            return sistema;
        }
        public void LimparEntidadesDeRelacionamento()
        {
            foreach (var item in PlanosFacade)
                item.LimparEntidadesDeRelacionamento();

            foreach (var item in ParametrosFacade)
                item.LimparEntidadesDeRelacionamento();

            foreach (var item in ConfiguracoesDoSistemaFacade)
                item.LimparEntidadesDeRelacionamento();
        }


        public int? Id { get; private set; }

        public string CodigoInterno { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }


        public virtual IReadOnlyCollection<PlanoDoSistema>? Planos { get; private set; }
        private ICollection<PlanoDoSistema>? PlanosFacade
        {
            get => Planos as ICollection<PlanoDoSistema>;
            set => Planos = value as IReadOnlyCollection<PlanoDoSistema>;
        }
        public void AdicionarPlano(PlanoDoSistema plano)
        {
            if (Planos == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Planos)}' na entidade '{Nome}' antes de continuar.");

            if ((Id != null && (plano.Sistema?.Id ?? plano.Sistema_Id) != Id) || plano.Sistema != this)
                throw new ArgumentException($"O plano '{plano.Nome}' foi criado para outro sistema: '{plano.Sistema?.Nome ?? plano.Sistema_Id.ToString()}'.");

            if (Planos.Any(r => r.GetHashCode() == plano.GetHashCode()))
                throw new ArgumentException($"O plano '{plano.Nome}' já existe no sistema '{Nome}'.");

            PlanosFacade.Add(plano);
        }


        public virtual IReadOnlyCollection<ParametroDoSistema>? Parametros { get; private set; }
        private ICollection<ParametroDoSistema>? ParametrosFacade
        {
            get => Parametros as ICollection<ParametroDoSistema>;
            set => Parametros = value as IReadOnlyCollection<ParametroDoSistema>;
        }
        public void AdicionarParametro(ParametroDoSistema parametro)
        {
            if (Parametros == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Parametros)}' na entidade '{Nome}' antes de continuar.");

            if ((Id != null && (parametro.Sistema?.Id ?? parametro.Sistema_Id) != Id) || parametro.Sistema != this)
                throw new ArgumentException($"O parâmetro '{parametro.CodigoUnico}' foi criado para outro sistema: '{parametro.Sistema?.Nome ?? parametro.Sistema_Id.ToString()}'.");

            if (Parametros.Any(r => r.CodigoUnico == parametro.CodigoUnico))
                throw new ArgumentException($"O parâmetro '{parametro.CodigoUnico}' já existe no sistema '{Nome}'.");

            if (!parametro.NivelDeConta)
                AdicionarConfiguracao(new ConfiguracaoDeSistema(this, parametro));

            ParametrosFacade.Add(parametro);
        }
        public void AdicionarParametros(ParametroDoSistema[] parametros)
        {
            foreach (var parametro in parametros)
                AdicionarParametro(parametro);
        }


        public virtual IReadOnlyCollection<ConfiguracaoDeSistema>? ConfiguracoesDoSistema { get; private set; }
        private ICollection<ConfiguracaoDeSistema>? ConfiguracoesDoSistemaFacade
        {
            get => ConfiguracoesDoSistema as ICollection<ConfiguracaoDeSistema>;
            set => ConfiguracoesDoSistema = value as IReadOnlyCollection<ConfiguracaoDeSistema>;
        }
        private void AdicionarConfiguracao(ConfiguracaoDeSistema configuracao)
        {
            if (ConfiguracoesDoSistema == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(ConfiguracoesDoSistema)}' na entidade '{Nome}' antes de continuar.");

            if ((Id != null && (configuracao.Sistema?.Id ?? configuracao.Sistema_Id) != Id) || configuracao.Sistema != this)
                throw new ArgumentException($"O configuração de '{configuracao.Parametro?.CodigoUnico}' foi criado para outro sistema: '{configuracao.Sistema?.Nome ?? configuracao.Sistema_Id.ToString()}'.");

            if (ConfiguracoesDoSistema.Any(r => r.GetHashCode() == configuracao.GetHashCode()))
                throw new ArgumentException($"A configuração de '{configuracao.Parametro?.CodigoUnico}' já existe no sistema '{Nome}'.");

            ConfiguracoesDoSistemaFacade.Add(configuracao);
        }



        // Rel
        public virtual IReadOnlyCollection<ContaDeSistema>? Contas { get; private set; }
    }
}