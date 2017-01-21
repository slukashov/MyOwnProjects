using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.LessonStudentRepository.Interfaces
{
   public interface ILessonStudentReadRepository
    {
        Task<LessonStudent> GetStudentAttengingAtLesson(long lessonId, long studentId);
    }
}
