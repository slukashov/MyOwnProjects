using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.GroupRepositories.Interfaces;

namespace StudentProgress.Repository.GroupRepositories
{
    internal class GroupWriteRepository : IGroupWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Group> dataSet;

        public GroupWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Group>();
        }

        public Task AddAsync(Group group)
        {
            dataSet.Add(group);
            return databaseContext.SaveChangesAsync();
        }

       
        public  Task AppoitHeadmanAsync(Student newHeadman)
        {
                databaseContext.Update(newHeadman.Group);
                return databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long groupIdToDelete)
        {
            var groupToDelete = new Group
            {
                Id = groupIdToDelete
            };
            dataSet.Attach(groupToDelete);
            dataSet.Remove(groupToDelete);
            await databaseContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Student newHeadman)
        {
            databaseContext.Update(newHeadman.Group);
            return databaseContext.SaveChangesAsync();
        }
    }
}
