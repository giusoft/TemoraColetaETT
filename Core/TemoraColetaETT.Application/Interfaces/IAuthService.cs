using TemoraColetaETT.Application.DTOs;

namespace TemoraColetaETT.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> GetTokenAsync();
    }
}
