using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.FacutiesRepositories.Interfaces
{
    public interface IFacultyReadRepository
    {
        Task GetFacultyByNameAsync(string name);
        Task<Faculty[]> GetAllFaculties();
        Task<Faculty> GetFacultyByIdAsync(long longId);
    }
}
