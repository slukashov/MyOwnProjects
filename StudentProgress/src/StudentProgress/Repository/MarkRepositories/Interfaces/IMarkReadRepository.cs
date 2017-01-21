using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.MarkRepositories.Interfaces
{
   public interface IMarkReadRepository
    {
        Task<Mark> GetMarkAsync(long journalSheetId, long studentId, long lessonId);
        Task<Mark[]> GetStudentMarkByCurrentDisciplineAsync(long lessonId, long journalSheetId);
    }
}
