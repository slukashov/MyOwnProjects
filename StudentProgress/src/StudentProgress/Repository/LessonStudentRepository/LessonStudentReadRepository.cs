using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.LessonStudentRepository.Interfaces;

namespace StudentProgress.Repository.LessonStudentRepository
{
    internal class LessonStudentReadRepository : ILessonStudentReadRepository
    {
        private readonly DbSet<LessonStudent> dataSet;

        public LessonStudentReadRepository(DbContext context)
        {
            dataSet = context.Set<LessonStudent>();
        }

        public Task<LessonStudent> GetStudentAttengingAtLesson(long lessonId, long studentId)
        {
            return
                dataSet.FirstOrDefaultAsync(
                    lessonStudent => lessonStudent.LessonId == lessonId && lessonStudent.StudentId == studentId);
        }
    }
}
