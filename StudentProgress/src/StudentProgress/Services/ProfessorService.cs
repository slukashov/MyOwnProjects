using StudentProgress.Entities;
using StudentProgress.Repository.AccountRepositories.Interfaces;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;
using StudentProgress.Requests;
using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Repository.FacutiesRepositories.Interfaces;

namespace StudentProgress.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IFacultyReadRepository facultyReadRepository;
        private readonly IProfessorWriteRepository professorWriteRepository;
        private readonly IAccountWriteRepository accountWriteRepository;
        private readonly IAccountReadRepository accountReadRepository;
        private readonly IProfessorReadRepository professorReadRepository;

        public ProfessorService(IFacultyReadRepository facultyReadRepository,
                                IProfessorWriteRepository professorWriteRepository,
                                IAccountReadRepository accountReadRepository,
                                IAccountWriteRepository accountWriteRepository,
                                IProfessorReadRepository professorReadRepository,
                                  UserManager<IdentityUser> userManager)
        {
            this.facultyReadRepository = facultyReadRepository;
            this.professorWriteRepository = professorWriteRepository;
            this.accountWriteRepository = accountWriteRepository;
            this.accountReadRepository = accountReadRepository;
            this.professorReadRepository = professorReadRepository;
            this.userManager = userManager;
        }

        public Task CreateProfessor(CreateProfessorRequest request)
        {
            return Task.Run(async () =>
            {
                var faculty = await facultyReadRepository.GetFacultyByIdAsync(request.FacultyId);
                var account = new Account(request.Name, request.SecondName, request.Email, request.Password);
                var professor = new Professor(account, faculty);
                await AddAsync(professor);
                await professorWriteRepository.AddAsync(professor);
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                };
                await userManager.CreateAsync(user, request.Password);
                await userManager.AddToRoleAsync(user, "Professor");
            });
        }

        public async Task AddAsync(Professor professor)
        {
            await accountWriteRepository.AddAsync(professor.Account);
            var account = await accountReadRepository.GetAccountByEmailAsync(professor.Account.Email);
            professor.AccountId = account.Id;
        }

        public async Task DeleteAsync(long professorId)
        {
                var professor = await professorReadRepository.GetProfessorByIdAsync(professorId);
                await professorWriteRepository.DeleteAsync(professor);
        }

        public async Task UpdateFacultyOnProfessor(UpdateFacultyOnProfessorRequest request)
        {
            var professor = await professorReadRepository.GetProfessorByIdAsync(request.ProfessorId);
            professor.FacultyId = request.FacultyId;
            await professorWriteRepository.UpdateAsync(professor);
        }
    }
}
