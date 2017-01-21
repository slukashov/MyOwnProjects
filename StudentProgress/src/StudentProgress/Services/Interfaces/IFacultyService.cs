using System.Threading.Tasks;

namespace StudentProgress.Services.Interfaces
{
    public interface IFacultyService
    {
        Task UpdateFacultyNameAsync(long facultyId, string name);
    }
}
