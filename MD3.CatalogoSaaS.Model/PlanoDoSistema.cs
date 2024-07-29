namespace MD3.CatalogoSaaS.Model
{
    public class PlanoDoSistema
    {
        public PlanoDoSistema()
        {

        }
        public PlanoDoSistema(int? sistema_Id, string nome, string descricao)
        {
            if (sistema_Id == null || sistema_Id == 0)
                throw new ArgumentNullException(nameof(sistema_Id));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentNullException(nameof(descricao));

            Sistema_Id = sistema_Id;
            Nome = nome;
            Descricao = descricao;
        }
        public PlanoDoSistema(Sistema sistema, string nome, string descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentNullException(nameof(descricao));

            Sistema = sistema ?? throw new ArgumentNullException(nameof(sistema));
            Sistema_Id = sistema.Id;

            Nome = nome;
            Descricao = descricao;
        }


        public void LimparEntidadesDeRelacionamento()
        {
            if (Sistema_Id != null && Sistema_Id > 0)
                Sistema = null;
        }


        public int Id { get; private set; }

        public int? Sistema_Id { get; set; }
        public virtual Sistema? Sistema { get; private set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;



        // Rel
        public virtual IReadOnlyCollection<ContaDeSistema>? Contas { get; private set; }


        public override int GetHashCode()
        {
            return Nome.GetHashCode();
        }
    }
}
