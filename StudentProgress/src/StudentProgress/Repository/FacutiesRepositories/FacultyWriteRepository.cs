using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.FacutiesRepositories.Interfaces;

namespace StudentProgress.Repository.FacutiesRepositories
{
    internal class FacultyWriteRepository : IFacultyWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Faculty> dataSet;

        public FacultyWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Faculty>();
        }

        public Task AddAsync(Faculty faculty)
        {
            dataSet.Add(faculty);
            return databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long facultyId)
        {
            var faculty = new Faculty
            {
                Id = facultyId
            };
            dataSet.Attach(faculty);
            dataSet.Remove(faculty);
            await databaseContext.SaveChangesAsync();
           
        }

        public async Task UpdateAsync(Faculty faculty)
        {
            dataSet.Update(faculty);
            await databaseContext.SaveChangesAsync();
        }
    }
}
