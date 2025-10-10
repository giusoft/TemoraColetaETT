using TemoraColetaETT.Application.DTOs;
using TemoraColetaETT.Infrastructure.Services;

namespace TemoraColetaETT.Infrastructure.Tests
{
    [Trait("Category", "Integration")]
    public class AuthServiceLiveTests
    {
        private readonly AuthService _authService;

        public AuthServiceLiveTests()
        {
            // Carrega as variáveis de ambiente do arquivo .env
            // Env.Load();

            // Cria um HttpClient REAL que fará chamadas de rede
            var httpClient = new HttpClient();

            // Cria o serviço com o HttpClient real
            _authService = new AuthService(httpClient);
        }

        [Fact]
        public async Task LoginAsync_ComCredenciaisValidas_DeveRetornarToken()
        {
            // Arrange (Dado)
            // IMPORTANTE: Substitua com um CPF e SENHA que existam e sejam válidos na sua API.
            var loginRequest = new LoginRequestDto
            {
                Cpf = "06262658513",
                Senha = "SenhaSegura123"
            };

            // Act (Quando)
            var result = await _authService.LoginAsync(loginRequest);

            // Assert (Então)
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.NotEmpty(result.Token);
        }

        [Fact]
        public async Task LoginAsync_ComCredenciaisInvalidas_DeveLancarHttpRequestException()
        {
            // Arrange (Dado)
            // Use credenciais que você sabe que são inválidas.
            var loginRequest = new LoginRequestDto
            {
                Cpf = "00000000000",
                Senha = "senha_errada"
            };

            // Act & Assert (Quando e Então)
            // A API deve retornar um código de erro (como 400 Bad Request ou 401 Unauthorized).
            // O método EnsureSuccessStatusCode() no AuthService irá converter isso em uma exceção.
            var exception = await Assert.ThrowsAsync<HttpRequestException>(
                () => _authService.LoginAsync(loginRequest)
            );

            // Opcional: Verificar o código de status HTTP específico, se desejar.
            // Por exemplo, para login inválido, a API pode retornar 'Unauthorized'.
            Assert.Contains("Unauthorized", exception.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
