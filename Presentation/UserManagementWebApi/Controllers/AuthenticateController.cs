using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Users.Commands.Login;
using UserManagement.Application.Users.Commands.RegisterUser;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = await mediator.Send(new UserLoginRequest(model));
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto model)
        {
            var roles = new List<string> { UserRoleEnum.User.ToString() };
            var response = await mediator.Send(new RegisterUserRequest(model, roles));

            return CreatedAtAction(nameof(Register), new { email = model.Email, role = UserRoleEnum.User.ToString() }, response);
        }

        [HttpPost]
        [Route("register-admin")]
       // [Authorize(Roles = Roles.Admin)]//todo add configuration to enable /disable this api
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserRequestDto model)
        {
            var roles = new List<string> { UserRoleEnum.Admin.ToString() };
            var response = await mediator.Send(new RegisterUserRequest(model, roles));
            return CreatedAtAction(nameof(RegisterAdmin), new { email = model.Email, role = UserRoleEnum.Admin.ToString() }, response);
        }

       
    }
}
