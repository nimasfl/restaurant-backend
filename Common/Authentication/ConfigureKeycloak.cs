using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Common.Exceptions.Responses;

namespace Restaurant.Common.Authentication;

public static class ConfigureServiceAuthenticationExtension
{
    private static RsaSecurityKey BuildRSAKey(string publicKeyJWT)
    {
        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(
            source: Convert.FromBase64String(publicKeyJWT),
            bytesRead: out _
        );
        var IssuerSigningKey = new RsaSecurityKey(rsa);
        return IssuerSigningKey;
    }

    public static void ConfigureKeycloak(this IServiceCollection services, string keycloakPublicKey)
    {
        var AuthenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        AuthenticationBuilder.AddJwtBearer(options =>
            {
                #region [ JWT Token Validation ]

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = BuildRSAKey(keycloakPublicKey),
                    ValidateLifetime = true
                };

                #endregion

                #region [ Event Authentification Handlers ]

                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = c =>
                    {
                        AddAppIdentity(ref c);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx => throw new UnauthorizedException(),
                    OnChallenge = ctx => throw new UnauthorizedException(),
                    OnForbidden = _ => throw new ForbiddenException(),
                };

                #endregion
            })
            .AddScheme<XApiKeyHeaderSchemeOptions, CustomAuthenticationHandler>(XApiKeyHeaderSchemeOptions.DefaultScheme,
                null);
    }

    private static void AddAppIdentity(ref TokenValidatedContext c)
    {
        var claims = new List<Claim>
        {
            new("username", c.Principal?.Identity?.FindFirstValue("preferred_username") ?? string.Empty),
            new("name", c.Principal?.Identity?.Name ?? string.Empty),
            new("id", "some guid token from dynamics"),
        };
        var appIdentity = new ClaimsIdentity(claims)
        {
            Label = "App"
        };
        c.Principal?.AddIdentity(appIdentity);
    }
}
