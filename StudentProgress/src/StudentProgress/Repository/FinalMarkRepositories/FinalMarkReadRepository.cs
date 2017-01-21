using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.FinalMarkRepositories.Interfaces;

namespace StudentProgress.Repository.FinalMarkRepositories
{
    internal class FinalMarkReadRepository : IFinalMarkReadRepository
    {
        private readonly DbSet<FinalMark> dataSet;

        public FinalMarkReadRepository(DbContext context)
        {
            dataSet = context.Set<FinalMark>();
        }
        public Task<FinalMark> GetStudentFinalMarksByCurrentDisciplineAsync(long studentId, long journalSheetId)
        {
            return dataSet
                  .FirstOrDefaultAsync(finalMark => IsValid(finalMark, studentId, journalSheetId));
        }

        private static bool IsValid(FinalMark finalMark,long studentId, long journalSheetId)
        {
            return finalMark.JournalSheetId == journalSheetId && finalMark.StudentId == studentId;
        }
    }
}
