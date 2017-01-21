using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using System.Linq;
using System.Threading.Tasks;
using StudentProgress.Repository.MarkRepositories.Interfaces;

namespace StudentProgress.Repository.MarkRepositories
{
    internal class MarkReadRepository : IMarkReadRepository
    {
        public readonly DbSet<Mark> dataSet;

        public MarkReadRepository(DbContext context)
        {
            dataSet = context.Set<Mark>();
        }

        public Task<Mark> GetMarkAsync(long journalSheetId, long studentId, long lessonId)
        {
            return dataSet.FirstOrDefaultAsync(mark => IsValid(mark, journalSheetId, studentId, lessonId));
        }

        private static bool IsValid(Mark mark, long journalSheetId, long studentId, long lessonId)
        {
            return mark.JournalSheetId == journalSheetId &&
                mark.LessonId == lessonId &&
                mark.StudentId == studentId;

        }

        public Task<Mark[]> GetStudentMarkByCurrentDisciplineAsync(long lessonId, long journalSheetId)
        {
            return dataSet.Where(mark => mark.JournalSheetId.Value == journalSheetId && mark.LessonId.Value == lessonId).ToArrayAsync();
        }
    }
}
