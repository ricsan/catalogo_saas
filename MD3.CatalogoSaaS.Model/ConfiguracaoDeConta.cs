namespace MD3.CatalogoSaaS.Model
{
    /// <summary>
    /// Configurações gerenciadas pelo fornecedor do SaaS, configurações que o cliente (Entidade) não pode alterar.
    /// Configurações a nível de conta.
    /// </summary>
    public class ConfiguracaoDeConta
    {
        public ConfiguracaoDeConta()
        {

        }
        public ConfiguracaoDeConta(ContaDeSistema conta, ParametroDoSistema parametro)
        {
            Parametro = parametro ?? throw new ArgumentNullException(nameof(parametro));
            Parametro_Id = parametro.Id;

            Conta = conta ?? throw new ArgumentNullException(nameof(conta));
            Conta_Id = conta.Id;
        }

        /// <summary>
        /// Construtor com tipo primitivo é necessário para o funcionamento do Entity Framework!
        /// https://learn.microsoft.com/en-us/ef/core/modeling/constructors
        /// https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
        /// </summary>
        public ConfiguracaoDeConta(int? conta_Id, int? parametro_Id)
        {
            if (conta_Id == null || conta_Id == 0)
                throw new ArgumentNullException(nameof(conta_Id));

            if (parametro_Id == null || parametro_Id == 0)
                throw new ArgumentNullException(nameof(parametro_Id));

            Parametro_Id = parametro_Id;
            Conta_Id = conta_Id;
        }


        public void LimparEntidadesDeRelacionamento()
        {
            if (Conta_Id != null && Conta_Id > 0)
                Conta = null;

            if (Parametro_Id != null && Parametro_Id > 0)
                Parametro = null;
        }


        public int? Conta_Id { get; set; }
        public virtual ContaDeSistema? Conta { get; private set; }

        public int? Parametro_Id { get; set; }
        public virtual ParametroDoSistema? Parametro { get; private set; }


        public string? Valor { get; set; }
        public void DefinirValor(string? valor) => Valor = valor;
    }
}
