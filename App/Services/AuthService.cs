using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Entities;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Data.Models.Users.Dto;
using Restaurant.Common.Exceptions.Responses;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.App.Services.Authentication;

[Injectable]
public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly RestaurantDbContext _dbContext;

    public AuthService(IConfiguration configuration, RestaurantDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async Task<BaseUserModel> Login(LoginRequestDto dto, HttpResponse response)
    {
        try
        {
            var user = await GetUserByUsername(dto.Username);
            if (user == null)
            {
                throw new UnauthorizedException();
            }

            var passwordIsValid = VerifyPassword(dto.Password, user.HashedPassword);
            if (!passwordIsValid)
            {
                throw new UnauthorizedException();
            }

            SignAndAttachTokenToCookie(response, user.Id);
            return new BaseUserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username
            };
        }
        catch
        {
            throw new UnauthorizedException();
        }
    }

    private void SignAndAttachTokenToCookie(HttpResponse response, Guid userId)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler
            .CreateToken(new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:ValidAudience"] ?? string.Empty,
                Issuer = _configuration["JWT:ValidIssuer"] ?? string.Empty,
                SigningCredentials = signinCredentials,
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userId.ToString())
                })
            });
        var stringToken = tokenHandler.WriteToken(token);
        response.Cookies.Append("token", stringToken,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddDays(1),
            });
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }

    public async Task<BaseUserModel> Signup(SignupRequestDto dto, HttpResponse response)
    {
        var asd = await GetUserByUsername(dto.Username);
        if (asd != null)
        {
            throw new UnprocessableEntityException("username already exists");
        }
        var user = new User
        {
            Name = dto.Name,
            Username = dto.Username,
            HashedPassword = HashPassword(dto.Password),
            CreatedOn = DateTime.UtcNow,
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        SignAndAttachTokenToCookie(response, user.Id);
        return new BaseUserModel
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username
        };
    }

    private async Task<User?> GetUserByUsername(string username)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .Where(u => u.Username == username)
            .Select(u => new User
            {
                Id = u.Id,
                HashedPassword = u.HashedPassword,
                Name = u.Name,
                Username = u.Username,
            })
            .SingleOrDefaultAsync();
    }

    public async Task<Requester?> GetUserById(Guid userId)
    {
        return await _dbContext
            .Users.AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new Requester
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username
            })
            .SingleOrDefaultAsync();
    }
}
