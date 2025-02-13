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
		Task<string> Register(RegisterDto registerDto);
		Task<string> Login(LoginDto loginDto);
		Task<string> RefreshToAccessToken(string refreshToken);
	}
}
