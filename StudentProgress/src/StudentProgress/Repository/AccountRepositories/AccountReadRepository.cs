using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.AccountRepositories.Interfaces;

namespace StudentProgress.Repository.AccountRepositories
{
    internal class AccountReadRepository : IAccountReadRepository
    {
        private readonly DbSet<Account> dataSet;

        public AccountReadRepository(DbContext context)
        {
            dataSet = context.Set<Account>();
        }

        public Task<Account> GetAccountByEmailAsync(string email)
        {
            return dataSet.FirstOrDefaultAsync(account => account.Email.ToLower().Equals(email.ToLower()));
        }

        public Task<Account[]> GetAllAccounts()
        {
            return dataSet.ToArrayAsync();
        }

        public Task<Account> GetAccountById(long accountId)
        {
            return dataSet.FirstOrDefaultAsync(account => account.Id == accountId);
        }
    }
}
