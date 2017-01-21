using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.DisciplineRepositories.Interfaces
{
    public interface IDisciplineWriteRepository
    {
        Task AddAsync(Discipline discipline);
        Task UpdateAsync(Discipline discipline);
    }
}
