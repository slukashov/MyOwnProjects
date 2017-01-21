using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.GroupRepositories.Interfaces
{
    public interface IGroupWriteRepository
    {
        Task AppoitHeadmanAsync(Student student);
        Task AddAsync(Group inObject);
        Task DeleteAsync(long inObjectId);
        Task UpdateAsync(Student student);

    }
}
