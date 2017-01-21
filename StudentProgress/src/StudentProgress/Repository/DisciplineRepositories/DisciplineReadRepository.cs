using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.DisciplineRepositories.Interfaces;

namespace StudentProgress.Repository.DisciplineRepositories
{
    internal class DisciplineReadRepository : IDisciplineReadRepository
    {
        private readonly DbSet<Discipline> dataSet;

        public DisciplineReadRepository(DbContext context)
        {
            dataSet = context.Set<Discipline>();
        }

        public Task<Discipline> GetDisciplineByNameAsync(string name)
        {
            return dataSet.FirstOrDefaultAsync(discipline => discipline.Name.ToLower().Equals(name.ToLower()));
        }

        public Task<Discipline[]> GetAllDisciplinesAsync()
        {
            return dataSet.ToArrayAsync();
        }

        public Task<Discipline> GetDisciplineByIdAsync(long idOfDiscipline)
        {
            return dataSet.FirstOrDefaultAsync(discipline => discipline.Id == idOfDiscipline);
        }
    }
}
