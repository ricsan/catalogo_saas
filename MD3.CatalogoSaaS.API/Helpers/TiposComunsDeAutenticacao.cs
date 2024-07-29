namespace MD3.CatalogoSaaS.API.Helpers
{
    public readonly struct TiposComunsDeAutenticacao
    {
        /// <summary>
        /// Autenticação padrão com usuário e senha gerenciado por um Idp.
        /// </summary>
        public const string EMAIL_SENHA = "EMAIL_SENHA";

        /// <summary>
        /// Autenticação utilizando a Google como Idp.
        /// </summary>
        public const string GOOGLE = "GOOGLE";

        /// <summary>
        /// Autenticação utilizando a Meta como Idp.
        /// </summary>
        public const string META = "META";

        /// <summary>
        /// Autenticação utilizando um diretório do Azure AD como Idp.
        /// </summary>
        public const string AZURE_AD = "AZURE_AD";
    }
}
