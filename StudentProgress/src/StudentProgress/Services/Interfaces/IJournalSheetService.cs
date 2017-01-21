using StudentProgress.Entities;
using StudentProgress.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentProgress.Models;

namespace StudentProgress.Services.Interfaces
{
    public interface IJournalSheetService
    {
        Task CreateJournalSheetAsync(CreateJournalSheetRequest createJournalSheetRequest);
        Task FillJournalSheetAsync(StudentModel[] model);
        Task FillAddFinalMarkAsync(FinalMarkRequest addFinalMarkRequest);
        Task<List<FinalMark>> GetAllFinalMarkAsync(FinalMarkRequest getFinalMarkRequest);
        Task UpdateFinalMarksAsync(FinalMark[] updateFinalMarkRequest);
        Task<List<Mark[]>> GetAllMarksFromCurrentDisciplineAsync(GetMarkRequest getMarkRequest);
        Task<StudentModel[]> ConfigureStudentModels(long groupId, long journaSheetId);
    }
}
