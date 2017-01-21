using Microsoft.AspNetCore.Identity;
using StudentProgress.Authorization.Entities.Identity;
using System.Security.Claims;

namespace StudentProgress.Infrastructure.Wrappers
{
    public class SignInManegerWrapper
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public SignInManegerWrapper(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return signInManager.IsSignedIn(user);
        }
    }
}