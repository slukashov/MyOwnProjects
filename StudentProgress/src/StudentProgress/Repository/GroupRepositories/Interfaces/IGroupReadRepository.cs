using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.GroupRepositories.Interfaces
{
    public interface IGroupReadRepository
    {
        Task<Group> GetGroupByNameAsync(string name);
        Task<Group[]> GetAllGroupsFromFacultyAsync(long facultyId);
        Task<Group[]> GetAllGroupsAsync();
        Task<Group> GetGroupByIdAsync(long id);
    }
}
