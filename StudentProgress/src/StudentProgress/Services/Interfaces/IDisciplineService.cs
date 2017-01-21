using StudentProgress.Requests;
using System.Threading.Tasks;

namespace StudentProgress.Services.Interfaces
{
    public interface IDisciplineService
    {
        Task UpdateDiscipline(DisciplineRequest discipline);
    }
}
