using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Restaurant.Common.Authentication;

public static class AuthSchema
{
    public const string KeyCloak = JwtBearerDefaults.AuthenticationScheme;
    public const string ApiKey = XApiKeyHeaderSchemeOptions.DefaultScheme;
    public const string Mixed = $"{KeyCloak},{ApiKey}";
}
