using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.JournalSheetRepositories.Interfaces
{
   public interface IJournalSheetWriteRepository
    {
        Task AddAsync(JournalSheet inObject);
        Task DeleteAsync(long inObjectId);
        Task UpdateAsync(long inObjectId);
    }
}
