using System.Linq;
using System.Threading.Tasks;
using StudentProgress.Entities;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Repository.LessonRepositories.Interfaces;

namespace StudentProgress.Repository.LessonRepositories
{
    internal class LessonReadRepository : ILessonReadRepository
    {
        private readonly DbSet<Lesson> dataSet;

        public LessonReadRepository(DbContext context)
        {
            dataSet = context.Set<Lesson>();
        }

        public async Task<Lesson[]> GetLessonsFromCurrentJournalSheet(long journalSheetId)
        {
            var lessons = await dataSet
                .Where(lesson => lesson.JournalSheetId == journalSheetId).ToArrayAsync();
            return lessons;
        }
    }
}
