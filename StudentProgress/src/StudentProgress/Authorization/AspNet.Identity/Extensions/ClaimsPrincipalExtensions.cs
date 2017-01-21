using System;
using System.Linq;
using System.Security.Claims;

namespace StudentProgress.Authorization.AspNet.Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            var name = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName);
            if (name == null)
                return String.Empty;

            var surname = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname);
            if (surname == null)
                return String.Empty;

            return name.Value + " " + surname.Value;
        }
    }
}
