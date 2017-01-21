using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.JournalSheetRepositories.Interfaces
{
     public interface IJournalSheetReadRepository
    {
        Task<JournalSheet[]> GetAllJournalSheetsAsync(long id);
        Task<JournalSheet> GetJournalSheetByDisciplineNameAsync(string disciplineName);
        Task<JournalSheet> GetJournalSheetByIdAsync(long journalSheetId);
        Task<JournalSheet[]> GetJournalSheetsByDisciplineNameAsync(string disciplineName);
    }
}
