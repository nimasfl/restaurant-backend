using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Data.Models.Users.Dto;
using Restaurant.App.Services.Authentication;
using Restaurant.Common.Exceptions.Responses;

namespace Restaurant.App.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<BaseUserModel> Login([FromBody] LoginRequestDto dto)
    {
        return await _authService.Login(dto, Response);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<BaseUserModel> Signup([FromBody] SignupRequestDto dto)
    {
        return await _authService.Signup(dto, Response);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public void Logout()
    {
        Response.Cookies.Delete("token");
    }

    [HttpGet("[action]")]
    [Authorize]
    public BaseUserModel WhoAmI(Requester requester)
    {
        return new BaseUserModel
        {
            Id = requester.Id,
            Name = requester.Name,
            Username = requester.Username,
        };
    }
}
