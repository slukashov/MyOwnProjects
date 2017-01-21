using Microsoft.AspNetCore.Identity;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Entities;
using StudentProgress.Repository.AccountRepositories.Interfaces;
using StudentProgress.Repository.StudentRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Requests;

namespace StudentProgress.Services
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IStudentWriteRepository studentWriteRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IAccountWriteRepository accountWriteRepository;
        private readonly IAccountReadRepository accountReadRepository;

        public StudentService(IGroupReadRepository groupReadRepository,
                               IStudentWriteRepository studentWriteRepository,
                               IStudentReadRepository studentReadRepository,
                               IAccountWriteRepository accountWriteRepository,
                               IAccountReadRepository accountReadRepository,
                               UserManager<IdentityUser> userManager
                               )
        {
            this.groupReadRepository = groupReadRepository;
            this.studentWriteRepository = studentWriteRepository;
            this.studentReadRepository = studentReadRepository;
            this.accountReadRepository = accountReadRepository;
            this.accountWriteRepository = accountWriteRepository;
            this.userManager = userManager;
        }

        public async Task AddAsync(Student student)
        {
            await accountWriteRepository.AddAsync(student.Account);
            var account = await accountReadRepository.GetAccountByEmailAsync(student.Account.Email);
            student.AccountId = account.Id;
        }

        public Task CreateStudentAsync(CreateStudentRequset request)
        {
            return Task.Run(async () =>
            {
                var group = await groupReadRepository.GetGroupByIdAsync(request.GroupId);
                var account = new Account(request.Name, request.SecondName, request.Email, "student");
                var student = new Student(account, group);
                await AddAsync(student);
                await studentWriteRepository.AddAsync(student);
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email,                
                };
                await userManager.CreateAsync(user, "student");
                await userManager.AddToRoleAsync(user, "Student");
            });
        }

        public Task DeleteAsync(long studentId)
        {
            return Task.Run(async () =>
            {
                var studentToRemove = await studentReadRepository.GetStudentById(studentId);
                var studentAccount = await accountReadRepository.GetAccountById(studentToRemove.AccountId.Value);
                await studentWriteRepository.DeleteAsync(studentToRemove);
                await accountWriteRepository.DeleteAsync(studentAccount);
            });
        }
    }
}
