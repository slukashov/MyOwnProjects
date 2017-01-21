using Microsoft.AspNetCore.Mvc;
using StudentProgress.Entities;
using StudentProgress.Repository.AccountRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using StudentProgress.Requests;

namespace StudentProgress.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class AccountController : ApiController
    {

        private readonly IAccountReadRepository accountReadRepository;
        private readonly IAccountService accountService;

        public AccountController(IAccountReadRepository accountReadRepository,
                                 IAccountService accountService)     
        {
            this.accountReadRepository = accountReadRepository;
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<Account[]> GetAccounts()
        {
            return await accountReadRepository.GetAllAccounts();
        }

        [HttpPost] 
        public async Task SaveUser([FromBody] UpdateAccountRequest updateAccountRequest)
        {
            await accountService.UpdateAccountInformationAsync(updateAccountRequest);
        }

        [HttpPost]
        public async Task UpdatePassword(long id, string newPassword)
        {
            var account = await accountReadRepository.GetAccountById(id);
            await accountService.UpdatePassword(account, newPassword);
        }
    }
}
