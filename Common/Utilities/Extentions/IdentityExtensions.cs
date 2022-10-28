using System.Security.Claims;
using System.Security.Principal;

namespace Restaurant.Common
{
    public static class IdentityExtensions
    {
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            var firstClaim = identity?.FindFirst(claimType);
            return firstClaim == null ? string.Empty : firstClaim.Value;
        }

        public static string FindFirstValue(this IIdentity identity, string claimType)
        {
            var claimsIdentity = (ClaimsIdentity?)identity;
            if (claimsIdentity == null)
            {
                return string.Empty;
            }
            return claimsIdentity.FindFirstValue(claimType);
        }
    }
}
