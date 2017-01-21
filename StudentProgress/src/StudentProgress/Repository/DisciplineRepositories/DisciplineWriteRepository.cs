using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.DisciplineRepositories.Interfaces;

namespace StudentProgress.Repository.DisciplineRepositories
{
    internal class DisciplineWriteRepository : IDisciplineWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Discipline> dataSet;

        public DisciplineWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Discipline>();
        }

        public Task AddAsync(Discipline discipline)
        {
            dataSet.Add(discipline);
            return databaseContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Discipline discipline)
        {
            dataSet.Update(discipline);
            return databaseContext.SaveChangesAsync();
        }
    }
}
