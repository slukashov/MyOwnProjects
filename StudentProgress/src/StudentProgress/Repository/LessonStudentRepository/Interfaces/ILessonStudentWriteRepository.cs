using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.LessonStudentRepository.Interfaces
{
    public interface ILessonStudentWriteRepository
    {
        Task AddAsync(LessonStudent studentLesson);
        Task AddRangeAsync(LessonStudent[] studentLesson);
        Task AddOrModify(LessonStudent lessonStudent);
    }
}
