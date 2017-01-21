using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
using StudentProgress.Repository.MarkRepositories.Interfaces;
using System.Linq;

namespace StudentProgress.Repository.MarkRepositories
{
    public class MarkWriteRepository : IMarkWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<Mark> dataSet;

        public MarkWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<Mark>();
        }

        public Task AddMarkAsync(Mark mark)
        {
            dataSet.Add(mark);
            return databaseContext.SaveChangesAsync();
        }

        public Task AddMarksAsync(Mark[] marks)
        {
            dataSet.AddRange(marks);
            return databaseContext.SaveChangesAsync();
        }

        public Task AddOrModify(Mark mark)
        {
            databaseContext.Entry(mark).State = IsStateModified(mark);
            return databaseContext.SaveChangesAsync();
        }

        private EntityState IsStateModified(Mark mark)
        {
            if (dataSet.Any(entity => IsValid(entity, mark)))
                
                return EntityState.Modified;

            return EntityState.Added;
        }

        private static bool IsValid(Mark entity, Mark mark)
        {
            if (entity.JournalSheetId == mark.JournalSheetId && entity.StudentId == mark.StudentId &&
                entity.LessonId == mark.LessonId)
            {
                mark.Id = entity.Id;
                return true;
            }
            return false;
        }
    }
}
