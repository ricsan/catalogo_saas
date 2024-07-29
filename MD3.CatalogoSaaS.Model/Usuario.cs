namespace MD3.CatalogoSaaS.Model
{
    /// <summary>
    /// - O primeiro item a ser criado no fluxo. É independente de Tenant e System. É criado aqui e no IdP.
    /// - O usuário neste catálogo serve para saber com quais contas e tenants ele tem relação.
    /// - O usuário manterá os mesmos logins independente da conta acessada. A autenticação é tratada no IdP.
    /// - O sistema de autorização (permissões) fica por conta do sistema: se vai ser roles, claims, policies ou algo personalizado.
    /// </summary>
    public class Usuario
    {
        public Usuario() { }
        public Usuario(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            Nome = nome;
            Email = email;
        }
        public static Usuario New(int id) => new() { Id = id };


        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public DateTimeOffset DataDeCadastro { get; private set; }
        public DateTimeOffset? DataDeAlteracao { get; private set; }


        private List<string> idpIds = new List<string>();
        public void AdicionarIdpId(string idpId)
        {
            if (!idpIds.Contains(idpId))
            {
                idpIds.Add(idpId);
                DatarAlteracao();
            }
        }
        public void RemoverIdpId(string idpId)
        {
            if (idpIds.Contains(idpId))
            {
                idpIds.Remove(idpId);
                DatarAlteracao();
            }
        }
        public string[] ObterListaDeIdpIds()
        {
            return idpIds.ToArray();
        }


        /// <summary>
        /// Coleção de IDs de usuário nos Idps utilizados para autenticação.
        /// Campo multivalorado!
        /// </summary>
        public string? IdpIds
        {
            get
            {
                if (idpIds == null || idpIds.Count == 0)
                    return null;

                return string.Join(',', idpIds);
            }
            set
            {
                if (value == null)
                    return;

                idpIds = value.Split('.').ToList();
            }
        }



        // Rel
        public virtual IReadOnlyCollection<ContaDeSistema>? Contas { get; private set; }


        public void DatarAlteracao()
        {
            if (Id != null)
                DataDeAlteracao = DateTimeOffset.Now;
        }
        public void DatarCadastro()
        {
            if (Id == null)
                DataDeCadastro = DateTimeOffset.Now;
        }
    }
}
