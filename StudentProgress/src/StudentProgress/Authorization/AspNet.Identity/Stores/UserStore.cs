using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentProgress.Authorization.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace StudentProgress.Authorization.AspNet.Identity.Stores
{
    public class UserStore<TUser, TRole, TContext> :
         IUserLoginStore<TUser>,
         IUserRoleStore<TUser>,
         IUserClaimStore<TUser>,
         IUserPasswordStore<TUser>,
         IUserSecurityStampStore<TUser>,
         IUserEmailStore<TUser>,
         IUserLockoutStore<TUser>,
         IUserPhoneNumberStore<TUser>,
         IQueryableUserStore<TUser>,
         IUserTwoFactorStore<TUser>
         where TUser : IdentityUser
         where TRole : IdentityRole
         where TContext : DbContext
    {
        private bool disposed;

        private readonly DbSet<TUser> userSet;
        private readonly DbSet<TRole> roles;
        private readonly DbSet<IdentityUserClaim> userClaims;
        private readonly DbSet<IdentityUserRole> userRoles;
        private readonly DbSet<IdentityUserLogin> userLogins;

        public UserStore(TContext context, IdentityErrorDescriber describer = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
                userSet = Context.Set<TUser>();
                roles = Context.Set<TRole>();
                userClaims = Context.Set<IdentityUserClaim>();
                userRoles = Context.Set<IdentityUserRole>();
                userLogins = Context.Set<IdentityUserLogin>();
            ErrorDescriber = describer ?? new IdentityErrorDescriber();
        }

        public TContext Context { get; }
        public IdentityErrorDescriber ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;
        public virtual IQueryable<TUser> Users => Context.Set<TUser>();

        public virtual Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public virtual Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public virtual Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public virtual Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public async virtual Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            userSet.Add(user);
            await SaveChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public async virtual Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            userSet.Attach(user);
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            Context.Update(user);

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return IdentityResult.Success;
        }

        public async virtual Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            userSet.Remove(user);

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return IdentityResult.Success;
        }

        public virtual Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var convertedUserId = Convert.ToInt32(userId);
            return Users.FirstOrDefaultAsync(user => user.Id == convertedUserId, cancellationToken);
        }

        public virtual Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Users.FirstOrDefaultAsync(user => user.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash != null);
        }

        public async virtual Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var roleEntity = await roles.SingleOrDefaultAsync(role => role.Name.ToUpper() == roleName.ToUpper(), cancellationToken);
            if (roleEntity == null)
                throw new InvalidOperationException("Role not found");

            var identityUserRole = new IdentityUserRole { UserId = user.Id, RoleId = roleEntity.Id };
            userRoles.Add(identityUserRole);
        }

       public async virtual Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var roleEntity = await roles.SingleOrDefaultAsync(role => role.Name.ToUpper() == roleName.ToUpper(), cancellationToken);
            if (roleEntity != null)
            {
                var userRole = await userRoles.FirstOrDefaultAsync(role => roleEntity.Id == role.RoleId && role.UserId == user.Id, cancellationToken);
                if (userRole != null)
                    userRoles.Remove(userRole);
            }
        }

        public virtual async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await userRoles
                .Join(roles, userRole => userRole.RoleId, role => role.Id, (userRole, role) => new { userRole, role })
                .Where(join => join.userRole.UserId == user.Id)
                .Select(join => join.role.Name)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var dbRole = await roles.SingleOrDefaultAsync(role => role.Name.ToUpper() == roleName.ToUpper(), cancellationToken);
            if (dbRole == null)
                return false;

            return await userRoles.AnyAsync(userRole => userRole.RoleId == dbRole.Id && userRole.UserId == user.Id, cancellationToken);
        }

        public async virtual Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

                var claims = await userClaims
                    .Where(userClaim => userClaim.UserId == user.Id)
                    .ToListAsync(cancellationToken);

                return await Task.FromResult(claims
                    .Select(userClaim => new Claim(userClaim.ClaimType, userClaim.ClaimValue))
                    .ToList());
        }

        public virtual Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            foreach (var claim in claims)
                userClaims.Add(new IdentityUserClaim { UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value });

            return Task.FromResult(false);
        }

        public async virtual Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            if (newClaim == null)
                throw new ArgumentNullException(nameof(newClaim));

            var matchedClaims = await userClaims
                .Where(userClaim => userClaim.UserId == user.Id && userClaim.ClaimValue == claim.Value && userClaim.ClaimType == claim.Type)
                .ToListAsync(cancellationToken);

            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimValue = newClaim.Value;
                matchedClaim.ClaimType = newClaim.Type;
            }
        }

        public async virtual Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            foreach (var claim in claims)
            {
                var matchedClaims = await userClaims
                    .Where(userClaim => userClaim.UserId == user.Id && userClaim.ClaimValue == claim.Value && userClaim.ClaimType == claim.Type)
                    .ToListAsync(cancellationToken);

                foreach (var userClaim in matchedClaims)
                    userClaims.Remove(userClaim);
            }
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (login == null)
                throw new ArgumentNullException(nameof(login));

            var identityUserLogin = new IdentityUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };

            userLogins.Add(identityUserLogin);
            return Task.FromResult(false);
        }

        public virtual async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var entry = await userLogins
                .SingleOrDefaultAsync(login => login.UserId == user.Id && login.LoginProvider == loginProvider && login.ProviderKey == providerKey, cancellationToken);

            if (entry != null)
                userLogins.Remove(entry);
        }

        public async virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var identityUserLogins = await userLogins
                .Where(login => login.UserId == user.Id)
                .ToListAsync(cancellationToken);

            return await Task.FromResult(identityUserLogins
                .Select(login => new UserLoginInfo(login.LoginProvider, login.ProviderKey, login.ProviderDisplayName))
                .ToList());
        }

        public async virtual Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var userLogin = await userLogins
                .FirstOrDefaultAsync(login => login.LoginProvider == loginProvider && login.ProviderKey == providerKey, cancellationToken);

            if (userLogin != null)
                return await Users.FirstOrDefaultAsync(user => user.Id == userLogin.UserId, cancellationToken);

            return null;
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.EmailConfirmed);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Email = email;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Email);
        }

        public virtual Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedEmail);
        }

        public virtual Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public virtual Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Users.FirstOrDefaultAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        public virtual Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnd);
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.LockoutEnd = lockoutEnd;
            return Task.FromResult(0);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public virtual Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public async virtual Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            return await userClaims
                .Join(Users, userclaims => userclaims.UserId, user => user.Id, (userclaims, user) => new { userclaims, user })
                .Where(join => join.userclaims.ClaimValue == claim.Value && join.userclaims.ClaimType == claim.Type)
                .Select(join => join.user)
                .ToListAsync(cancellationToken);
        }

        public async virtual Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (String.IsNullOrEmpty(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var dbRole = await roles.Where(role => role.Name == roleName).FirstOrDefaultAsync(cancellationToken);

            if (dbRole == null)
                return new List<TUser>();

            return await userRoles
                .Join(Users, userrole => userrole.UserId, user => user.Id, (userrole, user) => new { userrole, user })
                .Where(join => join.userrole.RoleId == dbRole.Id)
                .Select(join => join.user)
                .ToListAsync(cancellationToken);
        }

        public void Dispose()
        {
            disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        private Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.FromResult(0);
        }
    }
}
