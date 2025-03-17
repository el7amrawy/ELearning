using System.Security.Claims;

namespace ELearning.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal) {
            return int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        }
        //public static string GetProfileImage(this ClaimsPrincipal claimsPrincipal) {
        //    return claimsPrincipal.FindFirst("ProfileImage")?.Value;
        //}
        public static string GetUsername(this ClaimsPrincipal claimsPrincipal) {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
        }
    }
}
