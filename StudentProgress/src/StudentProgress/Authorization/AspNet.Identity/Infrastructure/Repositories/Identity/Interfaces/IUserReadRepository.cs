using System.Threading.Tasks;
using StudentProgress.Authorization.Entities.Identity;

namespace StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity.Interfaces
{
    public interface IUserReadRepository
    {
        Task<IdentityUser[]> SearchUsersAsync(string query, int? limit);
        Task<IdentityUser[]> GetAllUsers(int? limit);
        Task<IdentityUser> GetUserAsync(long id);
    }
}
