using Microsoft.AspNetCore.Mvc;
using StudentProgress.Entities;
using System.Web.Http;
using System.Threading.Tasks;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Repository.StudentRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using StudentProgress.Requests;

namespace StudentProgress.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class GroupController : ApiController
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IGroupWriteRepository groupWriteRepository;
        private readonly IAccountService accountService;
        private readonly IStudentService studentService;

        public GroupController(
                               IGroupReadRepository groupReadRepository,
                               IStudentReadRepository studentReadRepository,
                               IGroupWriteRepository groupWriteRepository,
                               IAccountService accountService,
                               IStudentService studentService)
        {
            this.groupWriteRepository = groupWriteRepository;
            this.groupReadRepository = groupReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.accountService = accountService;
            this.studentService = studentService;
        }

        [HttpPost]
        public async Task CreateGroup([FromBody] string name)
        {
            var group = new Group(name);
            await groupWriteRepository.AddAsync(group);
        }

        [HttpPost]
        public async Task CreateStudent([FromBody] CreateStudentRequset request)
        {
            await studentService.CreateStudentAsync(request);
        }

        [HttpPost]
        public async Task CreateHeadman([FromBody] AppointHeadmanRequest request)
        {
            await accountService.AppoitHeadmanAsync(request);
        }

        [HttpGet]
        public async Task<Group[]> GetGroup()
        {
            return await groupReadRepository.GetAllGroupsAsync();
        }

        [HttpPost]
        public async Task<Student[]> GetStudentsFromCurrentGroup([FromBody] long groupId)
        {
            return await studentReadRepository.GetStudentsFromCurrentGroupById(groupId);
        }

        [HttpPost]
        public async Task RemoveStudentFromCurrentGroup([FromBody] RemoveStudentRequest request)
        {
            await studentService.DeleteAsync(request.StudentId);
        }
    }
}
