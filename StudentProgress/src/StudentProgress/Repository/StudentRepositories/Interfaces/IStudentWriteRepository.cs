using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.StudentRepositories.Interfaces
{
    public interface IStudentWriteRepository
    {
        Task AddAsync(Student inObject);
        Task DeleteAsync(Student student);
        Task UpdateAsync(Student student);
    }
}
