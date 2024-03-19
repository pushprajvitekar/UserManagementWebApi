using LinqKit;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Contracts;
using UserManagement.Application.SortingPaging;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users;

namespace UserManagement.EFCorePersistence
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext dbContext;

        public UserRepository(UserManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserDto?> Get(IQueryObject<ApplicationUser> queryObject)
        {
            var usr = await dbContext.Users
                               .AsExpandable().FirstOrDefaultAsync(queryObject.Query());
            if(usr == null) { return null; }
            return new UserDto() { UserName = usr.UserName ?? "", Email = usr.Email ?? "" };
        }

        public async Task<Page<UserDto>> GetAll(IQueryObject<ApplicationUser> queryObject, SortingPagingDto sortingPagingDto)
        {
            var sortPage = sortingPagingDto ?? new SortingPagingDto();
            string sortBy = sortPage.SortBy ?? "Id";
            var usrs = await dbContext.Users
                                .AsExpandable().Where(queryObject.Query())
                                 .Select(u => new UserDto() { UserName = u.UserName ?? "", Email = u.Email ?? "" })
                                .ToPagedAsync(sortPage.PageNumber, sortPage.PageSize, sortBy, sortPage.SortAsc);
            return usrs;
        }
    }
}
