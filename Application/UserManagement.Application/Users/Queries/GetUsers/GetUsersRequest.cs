using MediatR;
using UserManagement.Application.SortingPaging;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users.Queries;

namespace UserManagement.Application.Users.Queries.GetUser
{
    public class GetUsersRequest : IRequest<Page<UserDto>>
    {
        public GetUsersRequest(UserFilter? filter, SortingPagingDto? sortingPaging) 
        {
            Filter = filter;
            SortingPaging = sortingPaging;
        }

        public UserFilter? Filter { get; }
        public SortingPagingDto? SortingPaging { get; }
    }
}
