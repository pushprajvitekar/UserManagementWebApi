using MediatR;
using UserManagement.Application.Contracts;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.QueryObjects;
using UserManagement.Domain.Users.Queries;

namespace UserManagement.Application.Users.Queries.GetUser
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto?>
    {
        private readonly IUserRepository userRepository;

        public GetUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<UserDto?> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var filter = new UserFilter() { UserName = request.UserName };
            var user = await userRepository.Get(new UsersFilterQueryObject(filter));
            return user;
        }
    }
}
