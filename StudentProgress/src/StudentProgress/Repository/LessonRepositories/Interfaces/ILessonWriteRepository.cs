using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.LessonRepositories.Interfaces
{
    public interface ILessonWriteRepository
    {
        Task AddAsync(Lesson inObject);
        Task DeleteAsync(long inObjectId);
        Task UpdateAsync(long inObjectId);
    }
}
