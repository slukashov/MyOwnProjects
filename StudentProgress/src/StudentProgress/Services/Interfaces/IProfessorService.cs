using StudentProgress.Requests;
using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Services.Interfaces
{
    public interface IProfessorService
    {
        Task CreateProfessor(CreateProfessorRequest professor);
        Task DeleteAsync(long professorId);
        Task AddAsync(Professor professor);
        Task UpdateFacultyOnProfessor(UpdateFacultyOnProfessorRequest request);
    }
}
