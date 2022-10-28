using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Restaurant.Common.Authentication;

public class CustomAuthenticationHandler : AuthenticationHandler<XApiKeyHeaderSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public CustomAuthenticationHandler(IOptionsMonitor<XApiKeyHeaderSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
    {
        _configuration = configuration;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.User.Identities.Any(i => i.Label == "App"))
        {
            return AuthenticateResult.NoResult();
        }

        if (!Request.Headers.TryGetValue(XApiKeyHeaderSchemeOptions.HeaderName, out var apiKey) || apiKey.Count != 1)
        {
            return AuthenticateResult.NoResult();
        }

        var apiKeys = _configuration.AsEnumerable().Where(e => e.Key.StartsWith("API_KEY")).Select(e => e.Value);
        if (!apiKeys.Contains(apiKey.First()))
        {
            return AuthenticateResult.NoResult();
        }

        var claims = new[] { new Claim(ClaimTypes.Name, "") };
        var identity = new ClaimsIdentity(claims, XApiKeyHeaderSchemeOptions.DefaultScheme);
        var identities = new List<ClaimsIdentity> { identity };
        var principal = new ClaimsPrincipal(identities);
        var ticket = new AuthenticationTicket(principal, XApiKeyHeaderSchemeOptions.DefaultScheme);
        return AuthenticateResult.Success(ticket);
    }
}
