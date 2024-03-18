using MediatR;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Commands.RegisterUser
{
    public class RegisterUserRequest : IRequest<RegisterUserResponseDto?>
    {
        public RegisterUserRequest(RegisterUserRequestDto registerUserRequestDto, IEnumerable<string> roles)
        {
            RegisterUserRequestDto = registerUserRequestDto;
            Roles = roles;
        }

        public RegisterUserRequestDto RegisterUserRequestDto { get; }
        public IEnumerable<string> Roles { get; }
    }
}
