namespace MD3.CatalogoSaaS.Model
{
    /// <summary>
    /// Entidade ou Organziação: Representa uma pessoa física ou jurídica, ou outra entidade de grupo (projeto, ideia de negócio, etc.).
    /// 
    /// - A relação de hierarquia indica uma holding ou grupo empresarial.
    /// - A holding/grupo pode forçar configurações sobre as subordinadas.
    /// - Uma entidade pode ter mais de uma conta no mesmo sistema.Ex.: UNITPAC pode precisar gerenciar outro portal no CMS.
    /// 
    /// OBS: Não é de responsabilidade do C.SaaS gerenciar dados pessoais e financeiros.
    /// </summary>
    public class Tenant
    {
        public Tenant(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            Nome = nome;
        }


        public Tenant() { }
        public static Tenant New(int id) => new Tenant { Id = id };
        public void LimparEntidadesDeRelacionamento()
        {
            if (Pai_Id != null && Pai_Id > 0)
                Pai = null;

            foreach (var item in Filhos)
                item.LimparEntidadesDeRelacionamento();
        }


        public int Id { get; set; }

        /// <summary>
        /// Campo para fazer o relacionamento dos dados desta entidade em outros sistemas (ERP, Financeiro, CRM, etc.).
        /// </summary>
        public string? IdGeral { get; set; }

        public string? Nome { get; set; }


        public int? Pai_Id { get; private set; }
        public virtual Tenant? Pai { get; private set; }
        public void DefinirPai(Tenant entidade)
        {
            if (Pai_Id == null && Pai == null)
                throw new ArgumentException($"A entidade {Nome} já tem um pai: {Pai?.Nome ?? Pai_Id?.ToString()}.");

            if (Filhos == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Filhos)}' na entidade '{Nome}' antes de continuar.");

            if (Filhos.Any()) //if (Filhos?.Any() ?? false)
                throw new ArgumentException($"A entidade {Nome} possui filhos.");

            if (entidade.Pai_Id != null || entidade.Pai != null)
                throw new ArgumentException($"A entidade passada como parâmetro {entidade.Nome} possui um pai.");

            Pai = entidade;
            Pai_Id = entidade.Id;
        }


        public virtual IReadOnlyCollection<Tenant>? Filhos { get; private set; }
        private ICollection<Tenant>? FilhosFacade
        {
            get => Filhos as ICollection<Tenant>;
            set => Filhos = value as IReadOnlyCollection<Tenant>;
        }
        public void AdicionarFilho(Tenant entidade)
        {
            if (Filhos == null)
                throw new ArgumentNullException($"Carregue a propriedade '{nameof(Filhos)}' na entidade '{Nome}' antes de continuar.");

            if (Filhos.Any(r => r.GetHashCode() == entidade.GetHashCode()))
                throw new ArgumentException($"A entidade {entidade.Nome} já faz parte do grupo {Nome}.");

            FilhosFacade.Add(entidade);
        }



        // Rel
        public virtual IReadOnlyCollection<ContaDeSistema>? Contas { get; private set; }
    }
}
