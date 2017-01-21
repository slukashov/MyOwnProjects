using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentProgress.Repository.DisciplineRepositories.Interfaces;
using StudentProgress.Entities;
using StudentProgress.Models;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;
using StudentProgress.Repository.LessonRepositories.Interfaces;
using StudentProgress.Repository.MarkRepositories.Interfaces;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;
using StudentProgress.Repository.StudentRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using StudentProgress.Requests;
using StudentProgress.Response;

namespace StudentProgress.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class JournalSheetController : Controller
    {
        private readonly IDisciplineWriteRepository disciplineWriteRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IProfessorReadRepository professorReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IDisciplineService disciplineService;
        private readonly IJournalSheetService journalSheetService;
        private readonly IJournalSheetReadRepository journalSheetReadRepository;
        private readonly ILessonService lessonServiceService;
        private readonly ILessonReadRepository lessonReadRepository;
        private readonly IMarkReadRepository markReadRepository;

        public JournalSheetController(IDisciplineWriteRepository disciplineWriteRepository,
                                      IDisciplineReadRepository disciplineReadRepository,
                                      IDisciplineService disciplineService,
                                      IProfessorReadRepository professorReadRepository,
                                      IGroupReadRepository groupReadRepository,
                                      IJournalSheetService journalSheetService,
                                      ILessonService lessonServiceService,
                                      IJournalSheetReadRepository journalSheetReadRepository,
                                      IStudentReadRepository studentReadRepository,
                                      ILessonReadRepository lessonReadRepository,
                                      IMarkReadRepository markReadRepository)
        {
            this.disciplineWriteRepository = disciplineWriteRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.professorReadRepository = professorReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.journalSheetService = journalSheetService;
            this.disciplineService = disciplineService;
            this.lessonServiceService = lessonServiceService;
            this.journalSheetReadRepository = journalSheetReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.markReadRepository = markReadRepository;
        }

        [HttpPost]
        public async Task CreateDiscipline([FromBody] string nameOfDiscipline)
        {
            var discipline = new Discipline(nameOfDiscipline);
            await disciplineWriteRepository.AddAsync(discipline);
        }

        [HttpGet]
        public async Task<Discipline[]> GetAllDisciplines()
        {
           return await disciplineReadRepository.GetAllDisciplinesAsync();
        }

        [HttpGet]
        public async Task<Professor[]> GetAllProfessors()
        {
            return await professorReadRepository.GetAllProfessorAsync();
        }

        [HttpGet]
        public async Task<Group[]> GetAllGroups()
        {
            return await groupReadRepository.GetAllGroupsAsync();
            
        }

        [HttpPost]
        public async Task CreateJournalSheet([FromBody] CreateJournalSheetRequest createJournalSheetRequest)
        {
            await journalSheetService.CreateJournalSheetAsync(createJournalSheetRequest);
        }

        [HttpPost]
        public async Task UpdateDiscipline([FromBody] DisciplineRequest discipline)
        {
            await disciplineService.UpdateDiscipline(discipline);
        }

        [HttpPost]
        public async Task CreateLesson([FromBody] LessonRequest request)
        {
            await lessonServiceService.CreateLesson(request);
        }

        [HttpPost]
        public async Task<List<JournalSheetResponse>> GetAllJournalSheets([FromBody] long groupId)
        {
            List<JournalSheetResponse> listJournalSheetResponse = new List<JournalSheetResponse>();
            var list = await journalSheetReadRepository.GetAllJournalSheetsAsync(groupId);
            foreach (var item in list)
            {
                listJournalSheetResponse.Add(new JournalSheetResponse { DisciplineName = item.Discipline.Name = (await disciplineReadRepository.GetDisciplineByIdAsync(item.DisciplineId.Value)).Name, JournaSheetId = item.Id});
            }
            return listJournalSheetResponse;
        }

        [HttpPost]
        public async Task<Student[]> GetAllStudentsFromCurrentGroup([FromBody] long groupId)
        {
            return await studentReadRepository.GetStudentsFromCurrentGroupById(groupId);
        }

        [HttpPost]
        public async Task FillAttendingInJournalSheet([FromBody] StudentModel[] studentModels)
        {
           await journalSheetService.FillJournalSheetAsync(studentModels);
        }

        [HttpPost]
        public async Task<StudentModel[]> GetLessonsFromCurrentJournalSheet([FromBody] ShowJournalSheetRequest request)
        {
            var list  = await journalSheetService.ConfigureStudentModels(request.GroupId,request.JournalSheetId);
            return list;
        }

        [HttpPost]
        public async Task FillFinalMarks([FromBody] FinalMarkRequest addFinalMarksRequest)
        {
            await journalSheetService.FillAddFinalMarkAsync(addFinalMarksRequest);
        }

        [HttpPost]
        public async Task<List<FinalMark>> GetFinalMarks([FromBody]FinalMarkRequest  getFinalMarksRequest)
        {
          return await journalSheetService.GetAllFinalMarkAsync(getFinalMarksRequest);
        }

        [HttpPost]
        public async Task UpdateFinalMarks([FromBody]FinalMark[] getFinalMarksRequest)
        {
            await journalSheetService.UpdateFinalMarksAsync(getFinalMarksRequest);
        }

        [HttpPost]
        public async Task<List<Mark[]>> GetMarks([FromBody]GetMarkRequest getFinalMarksRequest)
        {
            return await journalSheetService.GetAllMarksFromCurrentDisciplineAsync(getFinalMarksRequest);
        }
    }
}
