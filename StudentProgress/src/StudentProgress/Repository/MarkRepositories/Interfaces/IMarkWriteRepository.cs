using StudentProgress.Entities;
using System.Threading.Tasks;

namespace StudentProgress.Repository.MarkRepositories.Interfaces
{
     public interface IMarkWriteRepository
    {
        Task AddMarkAsync(Mark mark);
        Task AddMarksAsync(Mark[] marks);
        Task AddOrModify(Mark mark);
    }
}
