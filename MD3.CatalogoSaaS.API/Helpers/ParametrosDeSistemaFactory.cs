using MD3.CatalogoSaaS.Model;

namespace MD3.CatalogoSaaS.API.Helpers
{
    public static class ParametrosDeSistemaFactory
    {
        public static ParametroDoSistema GetDbConnectionStringParam(Sistema sistema)
        {
            return new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.DB_CONNECTION_STRING,
                true
            );
        }

        public static ParametroDoSistema GetAuthTypeParam(Sistema sistema)
        {
            return new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AUTH_TYPE,
                true
            );
        }

        public static ParametroDoSistema GetSystemAuthTypeParam(Sistema sistema)
        {
            return new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.SYSTEM_AUTH_TYPE,
                false
            );
        }

        public static ParametroDoSistema[] GetAwsS3CommonParams(Sistema sistema)
        {
            var bucketNameParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_NAME,
                true
            );

            var bucketAccessKeyParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_ACCESS_KEY,
                true
            );

            var bucketSecretKeyParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_SECRET_KEY,
                true
            );

            var bucketRegionParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_REGION,
                true
            );

            var bucketDomainParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_DOMAIN,
                true
            );

            return new ParametroDoSistema[]
            {
                bucketNameParam,
                bucketAccessKeyParam,
                bucketSecretKeyParam,
                bucketRegionParam,
                bucketDomainParam
            };
        }

        public static ParametroDoSistema[] GetAzureAdCommonParams(Sistema sistema)
        {
            var clientIdParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AZURE_AD_CLIENT_ID,
                false
            );

            var authorityParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AZURE_AD_AUTHORITY,
                false
            );

            var redirectUriParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AZURE_AD_REDIRECT_URI,
                false
            );

            return new ParametroDoSistema[]
            {
                clientIdParam,
                authorityParam,
                redirectUriParam
            };
        }

        public static ParametroDoSistema[] GetAuth0CommonParams(Sistema sistema)
        {
            var domainParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_DOMAIN,
                false
            );

            var clientIdParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_CLIENT_ID,
                false
            );

            var redirectUriParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_REDIRECT_URI,
                false
            );

            var logoutUriParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_LOGOUT_URI,
                false
            );

            return new ParametroDoSistema[]
            {
                domainParam,
                clientIdParam,
                redirectUriParam,
                logoutUriParam
            };
        }

        public static ParametroDoSistema[] GetAuth0APICommonParams(Sistema sistema)
        {
            var clientIdParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_API_CLIENT_ID,
                false
            );

            var secretParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_API_SECRET,
                false
            );

            var databaseParam = new ParametroDoSistema(
                sistema,
                CodigosComunsDeParametrosDeSistema.AUTH0_API_DATABASE,
                false
            );

            return new ParametroDoSistema[]
            {
                clientIdParam,
                secretParam,
                databaseParam
            };
        }
    }
}
