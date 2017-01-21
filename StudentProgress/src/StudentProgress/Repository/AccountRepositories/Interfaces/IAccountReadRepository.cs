using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.AccountRepositories.Interfaces
{
    public interface IAccountReadRepository
    {
        Task<Account> GetAccountByEmailAsync(string name);
        Task<Account[]> GetAllAccounts();
        Task<Account> GetAccountById(long accountId);
    }
}
