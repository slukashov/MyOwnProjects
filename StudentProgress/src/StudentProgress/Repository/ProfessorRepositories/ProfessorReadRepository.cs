using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;

namespace StudentProgress.Repository.ProfessorRepositories
{
    internal class ProfessorReadRepository : IProfessorReadRepository
    {
        public readonly DbSet<Professor> dataSet;

        public ProfessorReadRepository(DbContext context)
        {
            dataSet = context.Set<Professor>();
        }

        public Task<Professor[]> GetAllProfessorAsync()
        {
            return dataSet
                .Include(operation => operation.Account)
                .ToArrayAsync();
        }

        public Task GetProfessorByEmailAsync(string email)
        {
            return dataSet.FirstOrDefaultAsync(account => account.Account.Email.ToLower().Equals(email.ToLower()));
        }

        public Task<Professor> GetProfessorByIdAsync(long professorId)
        {
            return dataSet
                .Include(professor => professor.Account)
                .Include(professor => professor.Faculty)
                .FirstOrDefaultAsync(professor => professor.Id == professorId);
        }

        public async Task<Professor[]> GetProfessorFromCurrentFacultyById(long facultyId)
        {
            var faculty = await dataSet
                .Include(operation => operation.Account)
                .Where(professor => professor.FacultyId == facultyId).ToArrayAsync();

            return faculty;
        }
    }
}
