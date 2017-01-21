using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.LessonRepositories.Interfaces
{
    public interface ILessonReadRepository
    {
        Task<Lesson[]> GetLessonsFromCurrentJournalSheet(long journalSheetId);
    }
}
