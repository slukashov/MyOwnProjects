using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.ProfessorRepositories.Interfaces
{
    public interface IProfessorWriteRepository
    {
        Task AddAsync(Professor inObject);
        Task DeleteAsync(Professor professor);
        Task UpdateAsync(Professor professor);
    }
}
