using StudentProgress.Entities;
using System.Threading.Tasks;
using StudentProgress.Requests;

namespace StudentProgress.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudentAsync(CreateStudentRequset request); 
        Task DeleteAsync(long studentId);
        Task AddAsync(Student student);
    }
}
