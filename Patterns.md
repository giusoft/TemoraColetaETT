# **Arquitetura do Projeto**

## **Padrão MVVM**

* **Model**

  * Contém as **regras de negócio básicas** ou representações de dados que refletem o domínio da aplicação.
  * Pode incluir DTOs simples (Data Transfer Objects) ou modelos de entidades.

* **View**

  * É a **interface com o usuário (UI)**.
  * Responsável por:

    * Exibir informações.
    * Capturar entradas do usuário.
    * Mostrar mensagens de erro ou sucesso.
    * Gerenciar **estados visuais** da aplicação.
  * **Não deve conter lógica de negócio**, apenas lógica de apresentação e interação.

* **ViewModel**

  * Faz a **ponte entre a View e o Model**.
  * Interpreta ações do usuário (ex.: cliques, inputs).
  * Chama regras de negócio (via **Domain**) e atualiza propriedades **observáveis** que a View monitora.
  * Responsável pela **notificação de mudanças de estado** para que a View atualize automaticamente (Data Binding).

---

## **Padrão Clean Architecture**

* **Presentation**

  * Contém tudo relacionado à **UI** (no caso, **.NET MAUI**).
  * Define os **ViewModels**, **Views (XAML)** e eventuais **Bindings** com comandos.
  * Se comunica com a camada **Application**, **nunca diretamente com Domain ou Data**.

* **Application**

  * Contém **casos de uso**, **DTOs**, **interfaces de serviços** e **contratos**.
  * Define *como* o sistema deve funcionar, mas **não implementa detalhes concretos**.
  * O **ViewModel** acessa essa camada para executar ações do sistema (por exemplo, "Realizar Login", "Sincronizar Dados").

* **Domain**

  * Contém a **regra de negócio pura**:

    * Entidades.
    * Serviços de domínio.
    * Validações de regra.
  * Essa camada **não depende** de nenhuma outra (nem banco, nem UI).
  * Pode ser testada isoladamente (testes unitários simples e rápidos).

* **Data (Infrastructure)**

  * Contém **implementações concretas** de acesso a dados:

    * Banco de dados (SQLite, SQL Server, etc.)
    * API externas (HTTP, REST, gRPC, etc.)
    * Cache, Configurações remotas, Arquivos locais.
  * Essa camada **implementa as interfaces definidas na camada Application**.
  * Caso a API mude, apenas essa camada é alterada — mantendo as regras de negócio intactas.

---

## **Integração Clean Architecture + MVVM**

### Relação entre Camadas

| Clean Architecture | MVVM             | Responsabilidade                                                       |
| ------------------ | ---------------- | ---------------------------------------------------------------------- |
| Presentation       | View / ViewModel | Captura e apresenta dados, observa mudanças, aciona regras de negócio. |
| Application        | —                | Define os casos de uso e fluxos de aplicação.                          |
| Domain             | Model            | Regra de negócio e validação.                                          |
| Data               | —                | Persistência, comunicação com APIs e serviços externos.                |

### Fluxo de Execução (exemplo de requisição ao servidor)

1. **ViewModel** chama um caso de uso na camada **Application** (ex: `ObterUsuariosUseCase`).
2. O caso de uso aciona um **repositório** da camada **Data**.
3. O repositório faz uma **requisição HTTP** (ou executa código C++ nativo via binding, se aplicável).
4. A resposta é convertida (mapeada) para um **objeto de domínio** compreendido pela camada **Domain**.
5. O resultado é devolvido ao **ViewModel**, que atualiza propriedades observáveis.
6. A **View** é notificada e atualiza automaticamente a interface.

---

## **Integração com C++ (códigos externos)**

Em projetos híbridos .NET + C++, os módulos nativos devem ser **isolados e organizados na camada Data ou Infrastructure**, pois representam **detalhes técnicos de implementação**.

### Onde Colocar o Código C++:

```
Infrastructure/
└── TemoraColetaETT.Infrastructure.Native/
    ├── include/                  # Headers (.h)
    ├── src/                      # Implementações C++ (.cpp)
    ├── build/                    # Scripts de build (CMake, Makefile, etc.)
    ├── libTemoraNative.so/.dll   # Biblioteca compilada (dependendo da plataforma)
    ├── TemoraNativeWrapper.cs    # Wrapper C# para P/Invoke
```

### Como Integrar no .NET MAUI:

1. **Crie um wrapper em C#** usando `DllImport`:

   ```csharp
   internal static class NativeMethods
   {
       [DllImport("libTemoraNative", EntryPoint = "calcular_saldo", CallingConvention = CallingConvention.Cdecl)]
       public static extern double CalcularSaldo(double entrada, double saida);
   }
   ```

2. **Encapsule o uso do C++ na camada Infrastructure**:

   ```csharp
   public class NativeFinanceService : IFinanceService
   {
       public double CalcularSaldo(double entrada, double saida)
       {
           return NativeMethods.CalcularSaldo(entrada, saida);
       }
   }
   ```

3. **Registre essa implementação no container de injeção de dependência (DI)**:

   ```csharp
   builder.Services.AddSingleton<IFinanceService, NativeFinanceService>();
   ```

4. **O ViewModel nunca acessa diretamente o código nativo**, apenas o contrato (`IFinanceService`).

---

## **Estrutura Recomendada do Projeto**

```
TemoraColetaETT/
│
├── Core/
│   ├── TemoraColetaETT.Application/    # Casos de uso, DTOs, interfaces (contratos)
│   └── TemoraColetaETT.Domain/         # Entidades e regras de negócio puras
│
├── Infrastructure/
│   ├── TemoraColetaETT.Infrastructure/         # Banco, APIs, repositórios concretos
│   └── TemoraColetaETT.Infrastructure.Native/  # Código C++ nativo e wrappers P/Invoke
│
├── Presentation/
│   └── TemoraColetaETT.UI/             # Views (XAML), ViewModels, Bindings (.NET MAUI)
│
├── Tests/
│   ├── TemoraColetaETT.Application.Tests/
│   ├── TemoraColetaETT.Domain.Tests/
│   ├── TemoraColetaETT.Infrastructure.Tests/
│   └── TemoraColetaETT.UI.Tests/
│
└── TemoraColetaETT.sln                 # Solução principal do Visual Studio
```

---

## **Resumo Final**

* **Camada C++ (Infrastructure.Native)**: contém o código de alto desempenho, matemático, ou dependente de hardware.
* **Infraestrutura .NET (Infrastructure)**: traduz chamadas nativas para o domínio via interfaces.
* **Camadas superiores (Application / Presentation)**: permanecem completamente independentes de detalhes técnicos.
* **Benefício**: caso um módulo C++ seja substituído por outro (ou mesmo uma lib .NET pura), **nenhum código de regra de negócio ou UI precisará ser alterado**.

---

**Resultado:**
→ Arquitetura **limpa, modular e interoperável**, mantendo a **regra de negócio isolada** e facilitando testes, manutenção e integração com código nativo.
