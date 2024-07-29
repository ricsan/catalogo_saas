namespace MD3.CatalogoSaaS.Model
{
    /// <summary>
    /// Configurações gerenciadas pelo fornecedor do SaaS, configurações que o cliente (Entidade) não pode alterar.
    /// Configurações a nível de sistema.
    /// </summary>
    public class ConfiguracaoDeSistema
    {
        public ConfiguracaoDeSistema()
        {

        }
        public ConfiguracaoDeSistema(Sistema sistema, ParametroDoSistema parametro)
        {
            Parametro = parametro ?? throw new ArgumentNullException(nameof(parametro));
            Parametro_Id = parametro.Id;

            Sistema = sistema ?? throw new ArgumentNullException(nameof(sistema));
            Sistema_Id = sistema.Id;
        }

        /// <summary>
        /// Construtor com tipo primitivo é necessário para o funcionamento do Entity Framework!
        /// https://learn.microsoft.com/en-us/ef/core/modeling/constructors
        /// https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
        /// </summary>
        public ConfiguracaoDeSistema(int? sistema_Id, int? parametro_Id)
        {
            if (sistema_Id == null || sistema_Id == 0)
                throw new ArgumentNullException(nameof(sistema_Id));

            if (parametro_Id == null || parametro_Id == 0)
                throw new ArgumentNullException(nameof(parametro_Id));

            Parametro_Id = parametro_Id;
            Sistema_Id = sistema_Id;
        }


        public void LimparEntidadesDeRelacionamento()
        {
            if (Sistema_Id != null && Sistema_Id > 0)
                Sistema = null;

            if (Parametro_Id != null && Parametro_Id > 0)
                Parametro = null;
        }


        public int? Sistema_Id { get; set; }
        public virtual Sistema? Sistema { get; private set; }

        public int? Parametro_Id { get; set; }
        public virtual ParametroDoSistema? Parametro { get; private set; }


        public string? Valor { get; set; }
        public void DefinirValor(string? valor) => Valor = valor;


        public override int GetHashCode()
        {
            return Parametro?.Id is null && Parametro_Id is null && Sistema?.Id is null && Sistema_Id is null
                ? Parametro.GetHashCode()
                : $"{(Parametro?.Id ?? Parametro_Id)}{(Sistema?.Id ?? Sistema_Id)}".GetHashCode();
        }
    }
}
