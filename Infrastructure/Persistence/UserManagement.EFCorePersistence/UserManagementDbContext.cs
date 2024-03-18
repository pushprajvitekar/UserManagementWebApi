using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Users;

namespace UserManagement.EFCorePersistence
{
    public class UserManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {

        }
    }
}
