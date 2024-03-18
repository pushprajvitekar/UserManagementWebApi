using MediatR;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Commands.Login
{
    public class UserLoginRequest : IRequest<LoginResponseDto?>
    {
        public UserLoginRequest(LoginRequestDto loginRequestDto)
        {
            LoginRequestDto = loginRequestDto;
        }

        public LoginRequestDto LoginRequestDto { get; }
    }
}
