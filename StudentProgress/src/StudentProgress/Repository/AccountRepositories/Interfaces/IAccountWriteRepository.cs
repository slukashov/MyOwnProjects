using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.AccountRepositories.Interfaces
{
    public interface IAccountWriteRepository
    {
        Task UpdateAccount(Account account);
        Task AddAsync(Account account);
        Task DeleteAsync(Account account);
    }
}
