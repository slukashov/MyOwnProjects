using StudentProgress.Authorization.Entities.Identity;
using System.Linq;
using System.Security.Claims;

namespace StudentProgress.Authorization.AspNet.Identity.Extensions
{
    public static class IdentityUserExtensions
    {
        public static string GetFullName(this IdentityUser user)
        {
            return user.GetGivenName() + " " + user.GetSurname();
        }

        public static string GetGivenName(this IdentityUser user)
        {
            var name = user.Claims.FirstOrDefault(claim => claim.ClaimType == ClaimTypes.GivenName);
            return name == null ? string.Empty : name.ClaimValue;
        }

        public static string GetSurname(this IdentityUser user)
        {
            var surname = user.Claims.FirstOrDefault(claim => claim.ClaimType == ClaimTypes.Surname);
            return surname == null ? string.Empty : surname.ClaimValue;
        }
    }
}
