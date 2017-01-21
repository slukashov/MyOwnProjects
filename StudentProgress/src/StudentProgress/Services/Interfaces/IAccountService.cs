using StudentProgress.Entities;
using System.Threading.Tasks;
using StudentProgress.Requests;

namespace StudentProgress.Services.Interfaces
{
    public interface IAccountService
    {
        Task AppoitHeadmanAsync(AppointHeadmanRequest request);
        Task UpdatePassword(Account account, string password);
        Task UpdateAccountInformationAsync(UpdateAccountRequest updateAccountRequest);
    }
}
