using Microsoft.AspNetCore.Authentication;

namespace Restaurant.Common.Authentication;

public class XApiKeyHeaderSchemeOptions: AuthenticationSchemeOptions
{
        public const string DefaultScheme = "ApiKey";
        public const string HeaderName = "x-api-key";
}
