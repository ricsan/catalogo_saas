# Catalogo SaaS (CSaaS)

Catálogo para gerenciar sistemas, parâmetros, contas, tenants e planos.

Geralmente um SaaS é projetado para que cada ponto de processamento seja capaz de processar paralelamente requisições de diversas contas, sem que haja a necessidade de existir uma instância configurada para cada cliente. Isso pode acontecer com o software inteiro numa infra com escala vertical ou horizontal, ou com vários serviços separados que se comunicam entre sí. 

O propósito principal do CSaaS é servir ao sitema todas as configurações necessárias para que ele funcione corretamente, em qualquer servidor, em qualquer infra, sem necessidade de builds específicos ou configurações em arquivos para cada conta ou tenant.

Outro propósito é facilitar a criação de novas contas em sistemas para nvos clientes ou clientes já existente, podendo ser feita via UI ou automatizada pela API.

## Configurações comuns
- Domínio principal
- String de conexão
- Tipo de autenticação
- Configurações de autenticação
- Configurações de armazenamento (AWS S3)
- Estrutura de pastas

  O CSaaS foi projetado para ser flexível e aceitar qualquer tipo e quantidade de configuração.

## Versão beta (MVP)

- .NET 7.0
- Minimal API + Swagger
- Entity Framework
- ASP.NET Core
