using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.FinalMarkRepositories.Interfaces
{
    public interface IFinalMarkReadRepository
    {
        Task<FinalMark> GetStudentFinalMarksByCurrentDisciplineAsync(long studentId,long journalSheetId);
    }
}
