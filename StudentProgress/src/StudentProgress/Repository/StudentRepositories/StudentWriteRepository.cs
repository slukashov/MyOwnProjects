using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.StudentRepositories.Interfaces;

namespace StudentProgress.Repository.StudentRepositories
{
    internal class StudentWriteRepository : IStudentWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Student> dataSet;
        public StudentWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Student>();
        }

        public Task AddAsync(Student student)
        {
           dataSet.Add(student);
           return databaseContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Student student)
        {
            dataSet.Remove(student);
            return databaseContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
