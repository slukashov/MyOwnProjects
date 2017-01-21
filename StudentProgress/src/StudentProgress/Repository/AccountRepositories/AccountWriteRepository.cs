using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.AccountRepositories.Interfaces;

namespace StudentProgress.Repository.AccountRepositories
{
    internal class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Account> dataSet;

        public AccountWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Account>();
        }

        public Task AddAsync(Account account)
        {
            dataSet.Add(account);
            return databaseContext.SaveChangesAsync();
        }
       
        public async Task DeleteAsync(Account account)
        {
            dataSet.Attach(account);
            dataSet.Remove(account);
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            dataSet.Update(account);
            await databaseContext.SaveChangesAsync();
        }
    }
}
