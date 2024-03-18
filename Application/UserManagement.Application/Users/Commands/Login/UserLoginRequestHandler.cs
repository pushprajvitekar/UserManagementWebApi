using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users;

namespace UserManagement.Application.Users.Commands.Login
{
    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, LoginResponseDto?>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenProvider tokenProvider;

        public UserLoginRequestHandler(UserManager<ApplicationUser> userManager,ITokenProvider tokenProvider)
        {
            this.userManager = userManager;
            this.tokenProvider = tokenProvider;
        }
        public async Task<LoginResponseDto?> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.LoginRequestDto.UserName);
            if (user?.Email != null && await userManager.CheckPasswordAsync(user, request.LoginRequestDto.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var token = tokenProvider.CreateToken(user, userRoles);

                return new LoginResponseDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = token
                };
            }
            return null;
        }
    }
}
