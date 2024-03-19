using MediatR;
using UserManagement.Application.Contracts;
using UserManagement.Application.SortingPaging;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.QueryObjects;

namespace UserManagement.Application.Users.Queries.GetUser
{
    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest,Page<UserDto>>
    {
        private readonly IUserRepository userRepository;

        public GetUsersRequestHandler(IUserRepository userRepository  )
        {
            this.userRepository = userRepository;
        }
        public async Task<Page<UserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAll(new UsersFilterQueryObject(request.Filter),request.SortingPaging);
            return users;
        }
    }
}
