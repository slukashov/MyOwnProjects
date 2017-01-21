using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.LessonStudentRepository.Interfaces;
using System.Linq;

namespace StudentProgress.Repository.LessonStudentRepository
{
    internal class LessonStudentWriteRepository : ILessonStudentWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<LessonStudent> dataSet;

        public LessonStudentWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<LessonStudent>();
        }

        public Task AddRangeAsync(LessonStudent[] studentLessons)
        {
            dataSet.AddRange(studentLessons);
            return databaseContext.SaveChangesAsync();
        }

        public Task AddAsync(LessonStudent lessonStudent)
        {
            dataSet.Add(lessonStudent);
            return databaseContext.SaveChangesAsync();
        }

        public Task AddOrModify(LessonStudent lessonStudent)
        {
            databaseContext.Entry(lessonStudent).State = IsStateModified(lessonStudent);
            return databaseContext.SaveChangesAsync();
        }

        private EntityState IsStateModified(LessonStudent lessonStudent)
        {
            if (dataSet.Any(entity => IsValid(entity, lessonStudent)))
                return EntityState.Modified;

            return EntityState.Added;
        }

        private static bool IsValid(LessonStudent entity, LessonStudent lessonStudent)
        {
            if (entity.StudentId == lessonStudent.StudentId && entity.LessonId == lessonStudent.LessonId)
            {
                lessonStudent.LessonId = entity.LessonId;
                return true;
            }
            return false;
        }

    }
}
