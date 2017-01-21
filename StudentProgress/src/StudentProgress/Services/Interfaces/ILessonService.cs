using StudentProgress.Requests;
using System.Threading.Tasks;

namespace StudentProgress.Services.Interfaces
{
    public interface ILessonService
    {
        Task CreateLesson(LessonRequest lessonRequest);
    }
}
