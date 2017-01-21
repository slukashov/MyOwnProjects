using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.LessonRepositories.Interfaces;

namespace StudentProgress.Repository.LessonRepositories
{
    internal class LessonWriteRepository : ILessonWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Lesson> dataSet;

        public LessonWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Lesson>();
        }

        public Task DeleteAsync(long lessonId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long lessonId)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Lesson lesson)
        {
            dataSet.Add(lesson);
            return databaseContext.SaveChangesAsync();
        }

    }
}
