using System.Net.Http.Json;
using System.Text.Json;
using TemoraColetaETT.Application.DTOs;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Infrastructure.Configuration;

#if __MAUI__
using Microsoft.Maui.Storage;
#endif

namespace TemoraColetaETT.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var requestUrl = $"{Env.ApiBaseUrl}/auth/login";

            var response = await _httpClient.PostAsJsonAsync(requestUrl, loginRequest);
            response.EnsureSuccessStatusCode();

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (loginResponse == null)
            {
                throw new JsonException("A resposta do login foi nula.");
            }

            Console.WriteLine(
                JsonSerializer.Serialize(
                    new object[]
                    {
                    loginResponse
                    },
                    new JsonSerializerOptions { WriteIndented = true }
                )
            );

            return loginResponse;
        }

#if __MAUI__
        public Task<string?> GetTokenAsync() => SecureStorage.GetAsync("auth_token");
#else
        public Task<string?> GetTokenAsync() => Task.FromResult<string?>(null);
#endif
    }
}