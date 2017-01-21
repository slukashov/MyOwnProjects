using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.GroupRepositories.Interfaces;

namespace StudentProgress.Repository.GroupRepositories
{
    internal class GroupReadRepository : IGroupReadRepository
    {
        private readonly DbSet<Group> dataSet;

        public GroupReadRepository(DbContext context)
        {
            dataSet = context.Set<Group>();
        }

        public Task<Group> GetGroupByNameAsync(string name)
        {
            return dataSet.FirstOrDefaultAsync(group => group.Name.ToLower().Equals(name.ToLower()));
        }

        public Task<Group> GetGroupByIdAsync(long groupId)
        {
            return dataSet
                .Include(group => group.ListOfStudents)
                .Include(group => group.ListOfJournalSheets)
                .FirstOrDefaultAsync(group => group.Id == groupId);
        }

        public Task<Group[]> GetAllGroupsFromFacultyAsync(long facultyId)
        {
            return dataSet.Where(group => group.FacultyId == facultyId).ToArrayAsync();
        }

        public Task<Group[]> GetAllGroupsAsync()
        {
            return dataSet.ToArrayAsync();
        }
    }
}
