using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.StudentRepositories.Interfaces
{
    public interface IStudentReadRepository
    {
        Task GetStudentByEmailAsync(string name);
        Task<Student[]> GetStudentsFromCurrentGroupById(long groupId);
        Task<Student> GetStudentById(long id);
    }
}
