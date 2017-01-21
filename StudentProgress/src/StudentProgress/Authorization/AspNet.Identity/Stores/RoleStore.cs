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
    public class RoleStore<TRole, TContext> : IQueryableRoleStore<TRole>, IRoleClaimStore<TRole>
         where TRole : IdentityRole
         where TContext : DbContext
    {
        private bool disposed;
        private readonly DbSet<TRole> roleSet;
        private readonly DbSet<IdentityRoleClaim> roleClaimSet;

        public RoleStore(TContext context, IdentityErrorDescriber describer = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
                roleSet = Context.Set<TRole>();
                roleClaimSet = Context.Set<IdentityRoleClaim>();
            ErrorDescriber = describer ?? new IdentityErrorDescriber();
        }

        public TContext Context { get; }
        public IdentityErrorDescriber ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;
        public virtual IQueryable<TRole> Roles => Context.Set<TRole>();

        public async virtual Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            roleSet.Add(role);
            await SaveChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public async virtual Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            roleSet.Attach(role);
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            Context.Update(role);

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

        public async virtual Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            roleSet.Remove(role);

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

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;
            return Task.FromResult(0);
        }

        public virtual Task<TRole> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var convertedRoleId = Convert.ToInt32(id);
            return Roles.FirstOrDefaultAsync(role => role.Id == convertedRoleId, cancellationToken);
        }

        public virtual Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Roles.FirstOrDefaultAsync(role => role.NormalizedName == normalizedName, cancellationToken);
        }

        public virtual Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public virtual Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            var identityRoleClaims = await roleClaimSet
                .Where(roleClaim => roleClaim.RoleId == role.Id)
                .ToListAsync(cancellationToken);

            return await Task.FromResult(identityRoleClaims
                .Select(roleClaim => new Claim(roleClaim.ClaimType, roleClaim.ClaimValue))
                .ToList());
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            roleClaimSet.Add(new IdentityRoleClaim { RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value });

            return Task.FromResult(false);
        }

        public async Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            var claims = await roleClaimSet
                .Where(roleClaim => roleClaim.RoleId == role.Id && roleClaim.ClaimValue == claim.Value && roleClaim.ClaimType == claim.Type)
                .ToListAsync(cancellationToken);

            foreach (var identityRoleClaim in claims)
                roleClaimSet.Remove(identityRoleClaim);
        }

        public void Dispose()
        {
            disposed = true;
        }

        private async Task SaveChanges(CancellationToken cancellationToken)
        {
            if (AutoSaveChanges)
                await Context.SaveChangesAsync(cancellationToken);
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
