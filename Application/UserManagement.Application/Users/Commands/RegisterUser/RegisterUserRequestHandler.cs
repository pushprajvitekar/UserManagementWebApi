using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Exceptions;
using UserManagement.Domain.Users;

namespace UserManagement.Application.Users.Commands.RegisterUser
{
    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponseDto?>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterUserRequestHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<RegisterUserResponseDto?> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var model = request.RegisterUserRequestDto;
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                throw new ExistsException("User already exists!");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new DomainException("User creation failed! Please check user details and try again.", null, DomainErrorCode.InfrastructureError);
            var roles = request.Roles;
            await AddRoles(roles);
            foreach (var role in roles)
            {
                await userManager.AddToRoleAsync(user, role);
            }

            return new RegisterUserResponseDto(model.Username, model.Email, roles);
        }

        private async Task AddRoles(IEnumerable<string> roles)
        {
            var domainroles = Enum.GetNames(typeof(UserRoleEnum));

            foreach (var role in roles)
            {
                if (!domainroles.Contains(role))
                    throw new DomainException($"{role} is invalid.", null, DomainErrorCode.NotFound);
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper() });
            }
        }
    }
}
