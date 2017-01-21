using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.FacutiesRepositories.Interfaces
{
    public interface IFacultyWriteRepository
    {
        Task AddAsync(Faculty inObject);
        Task DeleteAsync(long inObjectId);
        Task UpdateAsync(Faculty inObject);
    }
}
