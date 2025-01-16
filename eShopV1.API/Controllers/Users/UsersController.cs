using Asp.Versioning;
using eShopv1.Domain.Abstractions;
using eShopV1.Application.Abstractions.Authentication;
using eShopV1.Application.Users.GetUserInfo;
using eShopV1.Application.Users.LoginUser;
using eShopV1.Application.Users.RegisterUser;
using eShopV1.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopV1.API.Controllers.Users
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;

        public UsersController(IMediator mediator, IUserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(request.Email, request.Password, request.FirstName , request.LastName, request.UserName);
            
            var result = await _mediator.Send(command, cancellationToken);
            
            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error.Code, message = result.Error.Name });
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<LoginUserResponse> result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("user-info")]
        [HasPermission(Permissions.UsersRead)]
        public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            var query = new GetUserInfoQuery();

            Result<GetUserInfoResponse> result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error.Code, message = result.Error.Name });
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpGet("is-authenticated")]
        public IActionResult IsAuthenticated()
        {   
            try
            {
                var userId = _userContext.UserId;
                return Ok(new { isAuthenticated = true });
            }
            catch (ApplicationException)
            {
                return Ok(new { isAuthenticated = false });
            }
        }
    }
}