using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentProgress.Context;

namespace StudentProgress.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateDbContextOptions).As<DbContextOptions<StudentProgressContext>>();
        }

        private static DbContextOptions<StudentProgressContext> CreateDbContextOptions(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();
            string connectionString = configuration["Data:DefaultConnection:ConnectionString"];
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<StudentProgressContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);

            return dbContextOptionsBuilder.Options;
        }
        
    }
}
