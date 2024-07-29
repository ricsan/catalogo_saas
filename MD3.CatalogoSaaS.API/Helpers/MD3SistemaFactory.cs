using MD3.CatalogoSaaS.Model;

namespace MD3.CatalogoSaaS.API.Helpers
{
    public static class MD3SistemaFactory
    {
        public static readonly string CodigoInternoDoCMS = "AA00";
        public static Sistema CMS()
        {
            var cms = Sistema.New(CodigoInternoDoCMS, "CMS", "Gestão de páginas e conteúdo");

            // Parâmetros a nível de conta
            cms.AdicionarParametro(ParametrosDeSistemaFactory.GetAuthTypeParam(cms));
            cms.AdicionarParametro(ParametrosDeSistemaFactory.GetDbConnectionStringParam(cms));
            cms.AdicionarParametros(ParametrosDeSistemaFactory.GetAwsS3CommonParams(cms));
            cms.AdicionarParametro(new ParametroDoSistema(cms, CodigosComunsDeParametrosDeConta.AZURE_AD_TENANT_ID, true));
            cms.AdicionarParametro(new ParametroDoSistema(cms, CodigosComunsDeParametrosDeConta.FRONT_DOMAIN, true));

            // Parâmetros a nível de sistema
            cms.AdicionarParametro(ParametrosDeSistemaFactory.GetSystemAuthTypeParam(cms));
            cms.AdicionarParametros(ParametrosDeSistemaFactory.GetAzureAdCommonParams(cms));
            cms.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0CommonParams(cms));
            cms.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0APICommonParams(cms));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "ImgDefault", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgTemp", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgConfigSite", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgConfigSiteSeo", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgBanner", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgNoticia", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgEvento", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgLink", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgEgressoAluno", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaDefaulAlbuns", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgSubsite", false));
            cms.AdicionarParametro(new ParametroDoSistema(cms, "PastaImgCurriculo", false));

            // Planos do sistema
            cms.AdicionarPlano(new PlanoDoSistema(cms, "Plano Básico", "Plano Inicial com recursos básicos."));
            cms.AdicionarPlano(new PlanoDoSistema(cms, "Plano Pró", "Plano Profissional com recursos avançados."));
            cms.AdicionarPlano(new PlanoDoSistema(cms, "Plano Enterprise", "Plano Enterprise com recursos Pró + atendimento prioritário."));


            // Definindo valores das configurações do sistema
            foreach (var configuracao in cms.ConfiguracoesDoSistema)
            {
                switch (configuracao.Parametro.CodigoUnico)
                {
                    // Configuração dos parâmetros básicos
                    case "ImgDefault":
                        configuracao.DefinirValor("/content/cms/imagens/semimagem.png");
                        break;
                    case "PastaImgTemp":
                        configuracao.DefinirValor("/imagens/temp/");
                        break;
                    case "PastaImgConfigSite":
                        configuracao.DefinirValor("/managed_images/configuracoes/");
                        break;
                    case "PastaImgConfigSiteSeo":
                        configuracao.DefinirValor("/managed_images/configuracoes/seo/");
                        break;
                    case "PastaImgBanner":
                        configuracao.DefinirValor("/managed_images/banners/");
                        break;
                    case "PastaImgNoticia":
                        configuracao.DefinirValor("/managed_images/noticias/");
                        break;
                    case "PastaImgEvento":
                        configuracao.DefinirValor("/managed_images/eventos/");
                        break;
                    case "PastaImgLink":
                        configuracao.DefinirValor("/managed_images/links/");
                        break;
                    case "PastaImgEgressoAluno":
                        configuracao.DefinirValor("/managed_images/egresso/alunos/");
                        break;
                    case "PastaDefaulAlbuns":
                        configuracao.DefinirValor("/managed_images/albuns/");
                        break;
                    case "PastaImgSubsite":
                        configuracao.DefinirValor("/managed_images/subsites/");
                        break;
                    case "PastaImgCurriculo":
                        configuracao.DefinirValor("/managed_images/curriculo/");
                        break;


                    // Configuração dos tipos de autenticação aceitadas
                    case CodigosComunsDeParametrosDeSistema.SYSTEM_AUTH_TYPE:
                        configuracao.DefinirValor($"{TiposComunsDeAutenticacao.EMAIL_SENHA},{TiposComunsDeAutenticacao.AZURE_AD}");
                        break;


                    // Configuração dos parâmetros do Azure AD
                    case CodigosComunsDeParametrosDeSistema.AZURE_AD_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AZURE_AD_AUTHORITY:
                        configuracao.DefinirValor("https://login.microsoftonline.com/organizations/v2.0");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AZURE_AD_REDIRECT_URI:
                        configuracao.DefinirValor("https://localhost:44346/triagem.aspx");
                        break;


                    // Configuração dos parâmetros do Auth0
                    case CodigosComunsDeParametrosDeSistema.AUTH0_DOMAIN:
                        configuracao.DefinirValor("md3dev.auth0.com");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_REDIRECT_URI:
                        configuracao.DefinirValor("https://localhost:44346/triagem.aspx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_LOGOUT_URI:
                        configuracao.DefinirValor("https://localhost:44346/logout");
                        break;


                    // Configuração dos parâmetros do Auth0 Manager API
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_SECRET:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_DATABASE:
                        configuracao.DefinirValor("Username-Password-Authentication");
                        break;
                }
            }

            return cms;
        }


        public static readonly string CodigoInternoDoRepositorio = "AA01";
        public static Sistema Repositorio()
        {
            var repo = Sistema.New(CodigoInternoDoRepositorio, "Repositório", "Gestão de arquivos e documentos");

            // Parâmetros a nível de conta
            repo.AdicionarParametro(ParametrosDeSistemaFactory.GetAuthTypeParam(repo));
            repo.AdicionarParametro(ParametrosDeSistemaFactory.GetDbConnectionStringParam(repo));
            repo.AdicionarParametros(ParametrosDeSistemaFactory.GetAwsS3CommonParams(repo));
            repo.AdicionarParametro(new ParametroDoSistema(repo, CodigosComunsDeParametrosDeConta.AZURE_AD_TENANT_ID, true));
            repo.AdicionarParametro(new ParametroDoSistema(repo, CodigosComunsDeParametrosDeConta.FRONT_DOMAIN, true));

            // Parâmetros a nível de sistema
            repo.AdicionarParametro(ParametrosDeSistemaFactory.GetSystemAuthTypeParam(repo));
            repo.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0CommonParams(repo));
            repo.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0APICommonParams(repo));
            repo.AdicionarParametro(new ParametroDoSistema(repo, "PastaImgMaterial", false));
            repo.AdicionarParametro(new ParametroDoSistema(repo, "PastaArquivoMaterial", false));

            // Planos do sistema
            repo.AdicionarPlano(new PlanoDoSistema(repo, "Plano Básico", "Plano Inicial com recursos básicos."));

            foreach (var configuracao in repo.ConfiguracoesDoSistema)
            {
                switch (configuracao.Parametro.CodigoUnico)
                {
                    // Configuração dos tipos de autenticação aceitadas
                    case CodigosComunsDeParametrosDeSistema.SYSTEM_AUTH_TYPE:
                        configuracao.DefinirValor($"{TiposComunsDeAutenticacao.EMAIL_SENHA},{TiposComunsDeAutenticacao.AZURE_AD}");
                        break;

                    case "PastaImgMaterial":
                        configuracao.DefinirValor($"/sistemas/{repo.CodigoInterno}/imagens/materiais/");
                        break;
                    case "PastaArquivoMaterial":
                        configuracao.DefinirValor($"/sistemas/{repo.CodigoInterno}/arquivos/materiais/");
                        break;


                    // Configuração dos parâmetros do Auth0
                    case CodigosComunsDeParametrosDeSistema.AUTH0_DOMAIN:
                        configuracao.DefinirValor("md3dev.auth0.com");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_REDIRECT_URI:
                        configuracao.DefinirValor("https://localhost:44346/triagem.aspx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_LOGOUT_URI:
                        configuracao.DefinirValor("https://localhost:44346/logout");
                        break;


                    // Configuração dos parâmetros do Auth0 Manager API
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_SECRET:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_DATABASE:
                        configuracao.DefinirValor("Username-Password-Authentication");
                        break;
                }
            }

            return repo;
        }


        public static readonly string CodigoInternoDoBlog = "AA02";
        public static Sistema Blog()
        {
            var blog = Sistema.New(CodigoInternoDoBlog, "Blog", "Sistema básico de Blog");

            // Parâmetros a nível de conta
            blog.AdicionarParametro(ParametrosDeSistemaFactory.GetAuthTypeParam(blog));
            blog.AdicionarParametro(ParametrosDeSistemaFactory.GetDbConnectionStringParam(blog));
            blog.AdicionarParametros(ParametrosDeSistemaFactory.GetAwsS3CommonParams(blog));
            blog.AdicionarParametro(new ParametroDoSistema(blog, CodigosComunsDeParametrosDeConta.AZURE_AD_TENANT_ID, true));
            blog.AdicionarParametro(new ParametroDoSistema(blog, CodigosComunsDeParametrosDeConta.FRONT_DOMAIN, true));

            // Parâmetros a nível de sistema
            blog.AdicionarParametro(ParametrosDeSistemaFactory.GetSystemAuthTypeParam(blog));
            blog.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0CommonParams(blog));
            blog.AdicionarParametros(ParametrosDeSistemaFactory.GetAuth0APICommonParams(blog));
            blog.AdicionarParametro(new ParametroDoSistema(blog, "PastaImgBlog", false));
            blog.AdicionarParametro(new ParametroDoSistema(blog, "PastaImgAutor", false));
            blog.AdicionarParametro(new ParametroDoSistema(blog, "PastaImgPublicacao", false));

            // Planos do sistema
            blog.AdicionarPlano(new PlanoDoSistema(blog, "Plano Básico", "Plano Inicial com recursos básicos."));

            foreach (var configuracao in blog.ConfiguracoesDoSistema)
            {
                switch (configuracao.Parametro.CodigoUnico)
                {
                    // Configuração dos tipos de autenticação aceitadas
                    case CodigosComunsDeParametrosDeSistema.SYSTEM_AUTH_TYPE:
                        configuracao.DefinirValor($"{TiposComunsDeAutenticacao.EMAIL_SENHA},{TiposComunsDeAutenticacao.AZURE_AD}");
                        break;

                    case "PastaImgBlog":
                        configuracao.DefinirValor($"/sistemas/{blog.CodigoInterno}/imagens/");
                        break;
                    case "PastaImgAutor":
                        configuracao.DefinirValor($"/sistemas/{blog.CodigoInterno}/imagens/autores/");
                        break;
                    case "PastaImgPublicacao":
                        configuracao.DefinirValor($"/sistemas/{blog.CodigoInterno}/imagens/publicacoes/");
                        break;


                    // Configuração dos parâmetros do Auth0
                    case CodigosComunsDeParametrosDeSistema.AUTH0_DOMAIN:
                        configuracao.DefinirValor("md3dev.auth0.com");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_REDIRECT_URI:
                        configuracao.DefinirValor("https://localhost:44346/triagem.aspx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_LOGOUT_URI:
                        configuracao.DefinirValor("https://localhost:44346/logout");
                        break;


                    // Configuração dos parâmetros do Auth0 Manager API
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_CLIENT_ID:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_SECRET:
                        configuracao.DefinirValor("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        break;
                    case CodigosComunsDeParametrosDeSistema.AUTH0_API_DATABASE:
                        configuracao.DefinirValor("Username-Password-Authentication");
                        break;
                }
            }

            return blog;
        }
    }
}
