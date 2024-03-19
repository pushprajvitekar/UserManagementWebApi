using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Queries.GetUser
{
    public class GetUserRequest : IRequest<UserDto?>
    {
        public GetUserRequest(string userName) 
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
