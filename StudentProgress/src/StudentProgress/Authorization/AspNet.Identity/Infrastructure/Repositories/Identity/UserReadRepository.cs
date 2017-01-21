using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity.Interfaces;
using StudentProgress.Authorization.Entities.Identity;

namespace StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity
{
    internal class UserReadRepository : IUserReadRepository
    {
        private readonly DbSet<IdentityUser> dataSet;

        public UserReadRepository(DbContext databaseContext)
        {
            dataSet = databaseContext.Set<IdentityUser>();
        }

        public Task<IdentityUser> GetUserAsync(long id)
        {
            return dataSet.FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<IdentityUser[]> SearchUsersAsync(string query, int? limit = null)
        {
            return DoSearchUsers(query, limit);
        }

        public Task<IdentityUser[]> GetAllUsers(int? limit = null)
        {
            return DoSearchUsers(limit: limit);
        }

        private Task<IdentityUser[]> DoSearchUsers(string query = null, int? limit = null)
        {
            var users = dataSet
                .Include(user => user.Claims)
                .Where(user => user.LockoutEnd == null);

            if (!String.IsNullOrWhiteSpace(query))
                users = users.Where(UserContainsQuery(query));

            users = users
                .OrderByDescending(user => user.Id);

            if (limit.HasValue)
                return users.Take(limit.Value).ToArrayAsync();

            return users.ToArrayAsync();
        }

        private static Expression<Func<IdentityUser, bool>> UserContainsQuery(string query)
        {
            var lowerQuery = query.ToLower();

            Expression<Func<IdentityUser, bool>> result = user =>
                user.Email.ToLower().Contains(lowerQuery) || user.Claims.Any(claim => claim.ClaimValue.ToLower().Contains(lowerQuery)); 

            return result;
        }
    }
}
