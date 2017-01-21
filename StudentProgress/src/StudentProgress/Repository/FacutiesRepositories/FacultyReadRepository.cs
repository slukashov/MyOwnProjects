using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.FacutiesRepositories.Interfaces;

namespace StudentProgress.Repository.FacutiesRepositories
{
    internal class FacultyReadRepository : IFacultyReadRepository
    {
        private readonly DbSet<Faculty> dataSet;

        public FacultyReadRepository(DbContext context)
        {
            dataSet = context.Set<Faculty>();
        }

        public Task<Faculty[]> GetAllFaculties()
        {
            return dataSet.ToArrayAsync();
        }

        public Task<Faculty> GetFacultyByIdAsync(long longId)
        {
            return dataSet.FirstOrDefaultAsync(faculty => faculty.Id == longId);
        }

        public Task GetFacultyByNameAsync(string name)
        {
           return dataSet.FirstOrDefaultAsync(faculty => faculty.Name.ToLower().Equals(name.ToLower()));
        }
        public Task<Faculty> GetFacultyById(long id)
        {
            return dataSet.FirstOrDefaultAsync(faculty => faculty.Id == id);
        }
    }
}
