using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentProgress.Entities;
using System.Web.Http;
using StudentProgress.Repository.FacutiesRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using StudentProgress.Requests;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;

namespace StudentProgress.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class FacultyController : ApiController
    {
        private readonly IFacultyWriteRepository facultyWriteRepository;
        private readonly IFacultyReadRepository facultyReadRepository;
        private readonly IProfessorService professorService;
        private readonly IFacultyService facultyService;
        private readonly IProfessorReadRepository professorReadRepository;

        public FacultyController(IFacultyReadRepository facultyReadRepository,
                                    IFacultyWriteRepository facultyWriteRepository,
                                    IProfessorService professorService,
                                    IFacultyService facultyService,
                                    IProfessorReadRepository professorReadRepository)
        {
            this.facultyWriteRepository = facultyWriteRepository;
            this.facultyReadRepository = facultyReadRepository;
            this.professorService = professorService;
            this.facultyService = facultyService;
            this.professorReadRepository = professorReadRepository;
        }

        [HttpPost]
        public async Task CreateFaculty([FromBody] string facultyName)
        {
            var faculty = new Faculty(facultyName);
            await facultyWriteRepository.AddAsync(faculty);
        } 

        [HttpGet]
        public async Task<Faculty[]> GetFaculties()
        {
            return await facultyReadRepository.GetAllFaculties();
        }

        [HttpPost]
        public async Task DeleteFaculty([FromBody] long facultyId)
        {
            await facultyWriteRepository.DeleteAsync(facultyId);
        }

        [HttpPost]
        public async Task CreateProfessor([FromBody] CreateProfessorRequest professor)
        {
            await professorService.CreateProfessor(professor);
        }

        [HttpPost]
        public async Task UpdateFaculty([FromBody] UpdateFacultyRequest updateFacultyRequest)
        {
            await facultyService.UpdateFacultyNameAsync(updateFacultyRequest.Id, updateFacultyRequest.Name);
        }

        [HttpPost]
        public async Task<Professor[]> GetProfessorFromCurrentFaculty([FromBody] long facultyId)
        {
            return await professorReadRepository.GetProfessorFromCurrentFacultyById(facultyId);
        }

        [HttpPost]
        public async Task DeleteProfessor([FromBody]RemoveProfessorRequest professorForRemove)
        {
                await professorService.DeleteAsync(professorForRemove.ProfessorId);
        }
        
        [HttpPost]
        public async Task UpdateFacultyOnProfessor([FromBody] UpdateFacultyOnProfessorRequest request)
        {
            await professorService.UpdateFacultyOnProfessor(request);
        }
    }
}
