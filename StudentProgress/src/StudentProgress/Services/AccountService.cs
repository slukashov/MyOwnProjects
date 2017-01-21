using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using StudentProgress.Repository.StudentRepositories.Interfaces;
using StudentProgress.Repository.AccountRepositories.Interfaces;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Entities;
using StudentProgress.Authorization.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using StudentProgress.Requests;

namespace StudentProgress.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IAccountWriteRepository accountWriteRepository;
        private readonly IAccountReadRepository accountReadRepository;
        private readonly IGroupWriteRepository groupWriteRepository;

        public AccountService(IGroupReadRepository groupReadRepository,
                               IStudentReadRepository studentReadRepository,
                               IGroupWriteRepository groupWriteRepository,
                               IAccountWriteRepository accountWriteRepository,
                               IAccountReadRepository accountReadRepository,
                               UserManager<IdentityUser> userManager)
        {
            this.groupWriteRepository = groupWriteRepository;
            this.groupReadRepository = groupReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.accountReadRepository = accountReadRepository;
            this.accountWriteRepository = accountWriteRepository;
            this.userManager = userManager;
        }

        public async Task AppoitHeadmanAsync(AppointHeadmanRequest request)
        {
            var newHeadMan = await studentReadRepository.GetStudentById(request.StudentId);
            newHeadMan.Account = await accountReadRepository.GetAccountById(newHeadMan.AccountId.Value);
            newHeadMan.Group = await groupReadRepository.GetGroupByIdAsync(newHeadMan.GroupId.Value);
            if (newHeadMan.Group.CapitainId != null)
            {
                var oldHeadman = await studentReadRepository.GetStudentById(newHeadMan.Group.CapitainId.Value);
                oldHeadman.Account = await accountReadRepository.GetAccountById(oldHeadman.AccountId.Value);

                await DisappointOldHeadman(oldHeadman, oldHeadman.Account.Password);
                await AppointNewHeadman(newHeadMan, request.Password);
            }
            else
            {
                await AppointNewHeadman(newHeadMan, request.Password);
            }
        }

        private async Task DisappointOldHeadman(Student oldHeadMan, string password)
        {
            await UpdatePassword(oldHeadMan.Account, password);
            var oldHeadmanAccount = await userManager.FindByEmailAsync(oldHeadMan.Account.Email);
            await ChangeRole(oldHeadmanAccount, "Headman", "Student");
        }


        private async Task AppointNewHeadman(Student newHeadMan, string password)
        {
            await UpdatePassword(newHeadMan.Account, password);
            newHeadMan.Group.CapitainId = newHeadMan.Id;
            await groupWriteRepository.AppoitHeadmanAsync(newHeadMan);
            var newHeadmanAccount = await userManager.FindByEmailAsync(newHeadMan.Account.Email);
            await ChangeRole(newHeadmanAccount, "Student", "Headman");
        }

        private async Task ChangeRole(IdentityUser studentToChanging, string oldRole, string newRole)
        {
            await userManager.RemoveFromRoleAsync(studentToChanging, oldRole);
            await userManager.AddToRoleAsync(studentToChanging, newRole);
        }


        public async Task UpdatePassword(Account account, string newPassword)
        {
            account.Password = newPassword;
            await accountWriteRepository.UpdateAccount(account);
        }

        public async Task UpdateAccountInformationAsync(UpdateAccountRequest updateAccountRequest)
        {
            var account = await accountReadRepository.GetAccountById(updateAccountRequest.Id);
            var userForNewPassword = await userManager.FindByEmailAsync(updateAccountRequest.Email);
            await userManager.ChangePasswordAsync(userForNewPassword, account.Password, updateAccountRequest.Password);

            account.Name = updateAccountRequest.Name;
            account.SerName = updateAccountRequest.SerName;
            account.Password = updateAccountRequest.Password;

            await accountWriteRepository.UpdateAccount(account);
        }
    }
}
