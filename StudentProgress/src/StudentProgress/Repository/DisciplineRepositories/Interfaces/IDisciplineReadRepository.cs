using System.Threading.Tasks;
using StudentProgress.Entities;

namespace StudentProgress.Repository.DisciplineRepositories.Interfaces
{
    public interface IDisciplineReadRepository
    {
        Task<Discipline> GetDisciplineByNameAsync(string name);
        Task<Discipline[]> GetAllDisciplinesAsync();
        Task<Discipline> GetDisciplineByIdAsync(long idOfDiscipline);
    }
}
