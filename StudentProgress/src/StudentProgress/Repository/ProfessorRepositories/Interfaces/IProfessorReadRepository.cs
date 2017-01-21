using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.ProfessorRepositories.Interfaces
{
    public interface IProfessorReadRepository
    {
        Task GetProfessorByEmailAsync(string name);
        Task<Professor[]> GetAllProfessorAsync();
        Task<Professor> GetProfessorByIdAsync(long professorId);
        Task<Professor[]> GetProfessorFromCurrentFacultyById(long facultyId);
    }
}
