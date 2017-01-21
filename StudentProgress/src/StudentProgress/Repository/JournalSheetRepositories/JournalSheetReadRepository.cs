using System.Linq;
using System.Threading.Tasks;
using StudentProgress.Entities;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;

namespace StudentProgress.Repository.JournalSheetRepositories
{
    internal class JournalSheetReadRepository : IJournalSheetReadRepository
    {
        private readonly DbSet<JournalSheet> dataSet;

        public JournalSheetReadRepository(DbContext context)
        {
            dataSet = context.Set<JournalSheet>();
        }

        public Task<JournalSheet[]> GetAllJournalSheetsAsync(long groupId)
        {
            return dataSet.Include(entity => entity.Discipline)
                .Where(js => js.GroupId == groupId)
                .ToArrayAsync();
        }

        public Task<JournalSheet> GetJournalSheetByDisciplineNameAsync(string disciplineName)
        {
            return dataSet.Include(operation => operation.Discipline)
                .FirstOrDefaultAsync(journalSheet => journalSheet.Discipline.Name.Equals(disciplineName));
        }

        public Task<JournalSheet[]> GetJournalSheetsByDisciplineNameAsync(string disciplineName)
        {
            return dataSet.Include(operation => operation.Discipline)
                .Where(journalSheet => journalSheet.Discipline.Name.Equals(disciplineName))
                .ToArrayAsync();
        }

        public Task<JournalSheet> GetJournalSheetByIdAsync(long journalSheetId)
        {
            return dataSet.Include(operation => operation.Discipline)
                .FirstOrDefaultAsync(journalSheet => journalSheet.Id == journalSheetId );
        }
    }
}
