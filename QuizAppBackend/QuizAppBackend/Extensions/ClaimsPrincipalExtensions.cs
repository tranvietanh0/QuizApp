using System.Security.Claims;

namespace QuizAppBackend.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User ID claim not found.");
        return int.Parse(claim.Value);
    }

    public static string GetUsername(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(ClaimTypes.Name)?.Value
            ?? throw new UnauthorizedAccessException("Username claim not found.");
    }
}
