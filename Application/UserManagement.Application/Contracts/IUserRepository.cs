using UserManagement.Application.SortingPaging;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users;

namespace UserManagement.Application.Contracts
{
    public interface IUserRepository
    {
        public Task<Page<UserDto>> GetAll(IQueryObject<ApplicationUser> queryObject, SortingPagingDto sortingPagingDto);

        public Task<UserDto?> Get(IQueryObject<ApplicationUser> queryObject);
    }
}
