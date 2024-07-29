namespace MD3.CatalogoSaaS.API.Helpers
{
    public readonly struct CodigosComunsDeParametrosDeConta
    {
        /// <summary>
        /// Domínios permitidos no front-end.
        /// Parâmetro multivalorado.
        /// </summary>
        public const string FRONT_DOMAIN = "FRONT_DOMAIN";



        /// <summary>
        /// String de conexão com o banco de dados.
        /// </summary>
        public const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";



        /// <summary>
        /// Informa o tipo de autenticação ativada para o tenant: usuário e senha, Azure AD, etc. 
        /// Parâmetro multivalorado.
        /// </summary>
        public const string AUTH_TYPE = "AUTH_TYPE";



        /// <summary>
        /// Caso a autenticação por Azure Ad esteja ativada, este é o ID do Tenant do cliente para ser verificado no login do usuário.
        /// </summary>
        public const string AZURE_AD_TENANT_ID = "AZURE_AD_TENANT_ID";



        /// <summary>
        /// Nome do bucket no S3.
        /// </summary>
        public const string AWS_S3_BUCKET_NAME = "AWS_S3_BUCKET_NAME";

        /// <summary>
        /// Chave de acesso do bucket no S3.
        /// </summary>
        public const string AWS_S3_BUCKET_ACCESS_KEY = "AWS_S3_BUCKET_ACCESS_KEY";

        /// <summary>
        /// Chave secreta do bucket no S3.
        /// </summary>
        public const string AWS_S3_BUCKET_SECRET_KEY = "AWS_S3_BUCKET_SECRET_KEY";

        /// <summary>
        /// Região de criação do bucket no S3.
        /// </summary>
        public const string AWS_S3_BUCKET_REGION = "AWS_S3_BUCKET_REGION";

        /// <summary>
        /// Domínio mapeado no DNS para o bucket.
        /// </summary>
        public const string AWS_S3_BUCKET_DOMAIN = "AWS_S3_BUCKET_DOMAIN";
    }
}
