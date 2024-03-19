using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.SortingPaging;
using UserManagement.Application.Users.Queries.GetUser;
using UserManagement.Domain.Users.Queries;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthorizationService authorizationService;

        public UsersController(IMediator mediator, IAuthorizationService authorizationService)
        {
            this.mediator = mediator;
            this.authorizationService = authorizationService;
        }
        // GET api/<UsersController>/5
        [HttpGet()]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter? filter, [FromQuery] SortingPagingDto? sortingPagingDto)
        {
            sortingPagingDto ??= new SortingPagingDto();
            var sortBy = !string.IsNullOrEmpty(sortingPagingDto.SortBy) ? sortingPagingDto.SortBy : "UserName";
            int pageNum = sortingPagingDto.PageNumber < 1 ? 1 : sortingPagingDto.PageNumber;
            int pageSze = sortingPagingDto.PageSize < 1 ? 10 : sortingPagingDto.PageSize;
            sortingPagingDto = new SortingPagingDto(sortBy, true, pageNum, pageSze);

            var res = await mediator.Send(new GetUsersRequest(filter, sortingPagingDto));
            return Ok(res);
        }

        // GET api/<UsersController>/5
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserDetails(string username)
        {
            var res = await mediator.Send(new GetUserRequest(username));
            return Ok(res);
        }
    }
}
