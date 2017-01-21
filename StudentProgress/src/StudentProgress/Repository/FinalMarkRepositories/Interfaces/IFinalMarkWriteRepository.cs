using StudentProgress.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentProgress.Repository.FinalMarkRepositories.Interfaces
{
   public interface IFinalMarkWriteRepository
    {
        Task AddAsync(FinalMark inObject);
        Task AddFinalMarksAsync(IEnumerable<FinalMark> finalMarks);
        Task UpdatedFinalMarksAsync(FinalMark[] finalMark);
    }
}
