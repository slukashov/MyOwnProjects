using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;

namespace StudentProgress.Repository.JournalSheetRepositories
{
    internal class JournalSheetWriteRepository : IJournalSheetWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<JournalSheet> dataSet;

        public JournalSheetWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<JournalSheet>();
        }

        public Task AddAsync(JournalSheet journalSheet)
        {
            dataSet.Add(journalSheet);
            return databaseContext.SaveChangesAsync();
        }

        public Task DeleteAsync(long journalSheetId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long inObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
