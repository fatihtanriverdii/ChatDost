using ChatAPI.Core.DTOs;

namespace ChatAPI.Core.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponseDto> Register(RegisterDto registerDto, CancellationToken cancellationToken);
		Task<AuthResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken);
		Task<AuthResponseDto> LoginAdminAsync(LoginDto loginDto, CancellationToken cancellationToken);
		Task<AuthResponseDto> RefreshToAccessToken(string refreshToken, CancellationToken cancellationToken);
	}
}
