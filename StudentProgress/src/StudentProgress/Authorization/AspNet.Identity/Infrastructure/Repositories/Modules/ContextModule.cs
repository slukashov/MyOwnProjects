using Autofac;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Context;

namespace StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Modules
{
    public class ContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<StudentProgressContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

        }
    }
}
