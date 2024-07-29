namespace MD3.CatalogoSaaS.API.Helpers
{
    public readonly struct CodigosComunsDeParametrosDeSistema
    {
        /// <summary>
        /// Tipos de autenticação com que o sistema trabalha. 
        /// Parâmetro multivalorado.
        /// </summary>
        public const string SYSTEM_AUTH_TYPE = "SYSTEM_AUTH_TYPE";



        /// <summary>
        /// Chave do app criado no Azure para gerenciar as autenticações.
        /// </summary>
        public const string AZURE_AD_CLIENT_ID = "AZURE_AD_CLIENT_ID";

        /// <summary>
        /// Autoridade usada para compor a requisição OID. Ex.: https://login.microsoftonline.com/{tenant-id}/v2.0.
        /// </summary>
        public const string AZURE_AD_AUTHORITY = "AZURE_AD_AUTHORITY";

        /// <summary>
        /// Url para redirecionamento após login e logout.
        /// </summary>
        public const string AZURE_AD_REDIRECT_URI = "AZURE_AD_REDIRECT_URI";



        public const string AUTH0_DOMAIN = "AUTH0_DOMAIN";
        public const string AUTH0_CLIENT_ID = "AUTH0_CLIENT_ID";
        public const string AUTH0_REDIRECT_URI = "AUTH0_REDIRECT_URI";
        public const string AUTH0_LOGOUT_URI = "AUTH0_LOGOUT_URI";

        public const string AUTH0_API_CLIENT_ID = "AUTH0_API_CLIENT_ID";
        public const string AUTH0_API_SECRET = "AUTH0_API_SECRET";
        public const string AUTH0_API_DATABASE = "AUTH0_API_DATABASE";
    }
}
