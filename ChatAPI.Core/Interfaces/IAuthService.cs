using ChatAPI.Core.DTOs;
using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponseDto> Register(RegisterDto registerDto, CancellationToken cancellationToken);
		Task<AuthResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken);
		Task<AuthResponseDto> RefreshToAccessToken(string refreshToken, CancellationToken cancellationToken);
	}
}
