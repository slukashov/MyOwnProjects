using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;

namespace StudentProgress.Repository.ProfessorRepositories
{
    internal class ProfessorWriteRepository : IProfessorWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Professor> dataSet;

        public ProfessorWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Professor>();
        }


        public Task AddAsync(Professor professor)
        {
            dataSet.Add(professor);
            return databaseContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Professor professor)
        {
            dataSet.Remove(professor);
            return databaseContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Professor professor)
        {
            dataSet.Update(professor);
            return databaseContext.SaveChangesAsync();
        }
    }
}
