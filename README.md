# TemoraColetaETT

Este repositório contém o código-fonte do projeto TemoraColetaETT, uma aplicação de coleta de dados construída com .NET 8. O projeto é estruturado seguindo princípios de arquitetura limpa (Clean Architecture) e o padrão MVVM para garantir uma separação clara de responsabilidades, manutenibilidade e testabilidade.

## Arquitetura

A arquitetura do projeto é baseada nos conceitos descritos no arquivo `Patterns.md`, aderindo à **Clean Architecture** combinada com o padrão **Model-View-ViewModel (MVVM)**. A solução é dividida em camadas distintas, cada uma com sua própria responsabilidade.

A regra principal de dependência é que as camadas internas (`Core`) não devem saber nada sobre as camadas externas (`Infrastructure`, `Presentation`).

1.  **Core**: O coração da aplicação.
    *   **Domain**: Contém as entidades de negócio (`Pessoa`, `Usuario`, etc.) e as regras de negócio mais puras, sem dependências externas.
    *   **Application**: Orquestra os casos de uso da aplicação. Define interfaces que são implementadas pelas camadas externas (como `IAuthService`) e utiliza os DTOs (Data Transfer Objects) para transferir dados entre as camadas.

2.  **Infrastructure**: Camada responsável por detalhes de implementação e integrações externas.
    *   Implementa as interfaces definidas na camada de `Application` (ex: `AuthService`).
    *   Gerencia o acesso a dados através do `AppDbContext` (provavelmente usando Entity Framework Core).
    *   Lida com serviços externos, como autenticação, logs e configuração de ambiente (através do arquivo `.env`).

3.  **Presentation**: A camada de interface com o usuário (UI), que utiliza o padrão MVVM.
    *   **View**: A interface da aplicação (.NET MAUI), responsável por capturar a entrada do usuário e exibir informações. Ela observa o `ViewModel`.
    *   **ViewModel**: Prepara os dados para a `View` e lida com as interações do usuário, acionando as regras de negócio na camada `Domain`.
    *   É uma aplicação **.NET MAUI**, o que a torna multiplataforma (Android, iOS, macOS, Windows).

4.  **Tests**: Contém os projetos de testes para garantir a qualidade e o comportamento esperado de cada camada.

## Estrutura de Pastas

A solução está organizada da seguinte forma:

```
/
├── Core/
│   ├── TemoraColetaETT.Application/   # Casos de uso, DTOs e interfaces
│   └── TemoraColetaETT.Domain/        # Entidades e regras de negócio
├── Infrastructure/
│   └── TemoraColetaETT.Infrastructure/  # Acesso a banco de dados, serviços externos
├── Presentation/
│   └── TemoraColetaETT.UI/            # Interface do usuário (.NET MAUI)
├── Tests/
│   ├── TemoraColetaETT.Application.Tests/
│   ├── TemoraColetaETT.Domain.Tests/
│   ├── TemoraColetaETT.Infrastructure.Tests/
│   └── TemoraColetaETT.UI.Tests/
└── TemoraColetaETT.sln                # Arquivo da solução Visual Studio
```

## Dependências entre Projetos

As dependências seguem um fluxo unidirecional para o centro da arquitetura:

*   **Presentation (UI)** depende de **Application**.
*   **Infrastructure** depende de **Application**.
*   **Application** depende de **Domain**.
*   **Domain** não depende de ninguém.

Isso garante que a lógica de negócio (`Domain`) e as regras da aplicação (`Application`) sejam independentes de detalhes de implementação como banco de dados ou interface gráfica.

## Camada de Testes

A pasta `Tests` é fundamental para o projeto e contém quatro projetos de teste, um para cada camada principal da aplicação:

*   `TemoraColetaETT.Application.Tests`: Testa os casos de uso e a lógica da aplicação.
*   `TemoraColetaETT.Domain.Tests`: Testa as entidades e regras de negócio puras.
*   `TemoraColetaETT.Infrastructure.Tests`: Testa as implementações da camada de infraestrutura, como a integração com o banco de dados (geralmente com mocks ou um banco de dados em memória).
*   `TemoraColetaETT.UI.Tests`: Testa os ViewModels e a lógica de apresentação.

## Configuração

A configuração de ambiente (como strings de conexão de banco de dados ou chaves de API) é gerenciada através de um arquivo `.env` localizado em `Infrastructure/TemoraColetaETT.Infrastructure/`. A classe `Env.cs` é responsável por carregar essas variáveis para a aplicação.

## Como Compilar e Executar

1.  Clone o repositório.
2.  Abra o arquivo `TemoraColetaETT.sln` no Visual Studio 2022 (ou superior).
3.  Configure o arquivo `.env` na camada de infraestrutura com as variáveis necessárias.
4.  Defina o projeto `TemoraColetaETT.UI` como projeto de inicialização.
5.  Compile e execute a aplicação.

Alternativamente, a compilação pode ser feita através da linha de comando usando o .NET SDK:
```bash
dotnet build TemoraColetaETT.sln
```
