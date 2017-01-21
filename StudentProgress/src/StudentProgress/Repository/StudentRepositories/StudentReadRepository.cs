using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.StudentRepositories.Interfaces;

namespace StudentProgress.Repository.StudentRepositories
{
    internal class StudentReadRepository : IStudentReadRepository
    {
        private readonly DbSet<Student> dataSet;
        public StudentReadRepository(DbContext context)
        {
           
            dataSet = context.Set<Student>();
        }
        public Task GetStudentByEmailAsync(string email)
        {
            return dataSet.FirstOrDefaultAsync(account => account.Account.Email.ToLower().Equals(email.ToLower()));
        }

        public Task<Student> GetStudentById(long studentId)
        {
            return dataSet.Include(entity => entity.Account).FirstOrDefaultAsync(student => student.Id == studentId);
        }

        public async Task<Student[]> GetStudentsFromCurrentGroupById(long groupId)
        {
            return await dataSet
                .Include(student => student.Account)
                .Where(student => student.GroupId == groupId).ToArrayAsync();
        }
    }
}
