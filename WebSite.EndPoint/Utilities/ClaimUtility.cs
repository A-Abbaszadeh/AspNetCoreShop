using System.Security.Claims;

namespace WebSite.EndPoint.Utilities
{
    public static class ClaimUtility
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            var claimIdentity = user.Identity as ClaimsIdentity;
            string userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
