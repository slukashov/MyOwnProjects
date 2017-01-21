using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Entities;
﻿using System.Collections.Generic;
using StudentProgress.Repository.FinalMarkRepositories.Interfaces;

namespace StudentProgress.Repository.FinalMarkRepositories
{
    internal class FinalMarkWriteRepository : IFinalMarkWriteRepository
    {
        private readonly DbContext databaseContext;
        private readonly DbSet<FinalMark> dataSet;

        public FinalMarkWriteRepository(DbContext databaseContext)
        {
            this.databaseContext = databaseContext;
            dataSet = databaseContext.Set<FinalMark>();
        }

        public Task AddAsync(FinalMark finalMark)
        {
            dataSet.Add(finalMark);
            return databaseContext.SaveChangesAsync();
        }
        
        public Task AddFinalMarksAsync(IEnumerable<FinalMark> finalMarks)
        {
            dataSet.AddRange(finalMarks);
            return databaseContext.SaveChangesAsync();
        }

        public Task UpdatedFinalMarksAsync(FinalMark[] finalMark)
        {
            dataSet.UpdateRange(finalMark);
            return databaseContext.SaveChangesAsync();
        }
    }
}
