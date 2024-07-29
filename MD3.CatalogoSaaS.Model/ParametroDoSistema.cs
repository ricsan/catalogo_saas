namespace MD3.CatalogoSaaS.Model
{
    /// <summary>
    /// Parâmetros de configurações gerenciadas pelo fornecedor do SaaS, configurações que o cliente (Entidade) não pode alterar.
    /// OBS: Dependendo do parâmetro alterado, o sistema precisará ser reiniciado!
    /// </summary>
    public class ParametroDoSistema
    {
        public ParametroDoSistema()
        {

        }
        public ParametroDoSistema(int? sistema_Id, string codigoUnico, bool nivelDeConta)
        {
            if (sistema_Id == null || sistema_Id == 0)
                throw new ArgumentNullException(nameof(sistema_Id));

            if (string.IsNullOrWhiteSpace(codigoUnico))
                throw new ArgumentNullException(nameof(codigoUnico));

            Sistema_Id = sistema_Id;
            CodigoUnico = codigoUnico;
            NivelDeConta = nivelDeConta;
        }
        public ParametroDoSistema(Sistema sistema, string codigoUnico, bool nivelDeConta)
        {
            if (string.IsNullOrWhiteSpace(codigoUnico))
                throw new ArgumentNullException(nameof(codigoUnico));

            Sistema = sistema ?? throw new ArgumentNullException(nameof(sistema));
            Sistema_Id = sistema.Id;

            CodigoUnico = codigoUnico;
            NivelDeConta = nivelDeConta;
        }

        public void LimparEntidadesDeRelacionamento()
        {
            if (Sistema_Id != null && Sistema_Id > 0)
                Sistema = null;
        }


        public int? Id { get; private set; }

        public int? Sistema_Id { get; set; }
        public virtual Sistema? Sistema { get; private set; }

        public string CodigoUnico { get; set; }

        /// <summary>
        /// Indica se o parâmetro será configurado a nível de conta do sistema. 
        /// Caso contrário, uma única configuração será feita para o sistema (todas as contas).
        /// </summary>
        public bool NivelDeConta { get; set; }


        // Rel
        public virtual IReadOnlyCollection<ConfiguracaoDeConta>? ConfiguracoesNasContas { get; private set; }
        public virtual IReadOnlyCollection<ConfiguracaoDeSistema>? ConfiguracoesNosSistemas { get; private set; }


        public override int GetHashCode()
        {
            return CodigoUnico.GetHashCode();
        }
    }
}
