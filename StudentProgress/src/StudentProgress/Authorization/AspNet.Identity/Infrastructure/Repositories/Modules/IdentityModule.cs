using Autofac;
using StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity;
using StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity.Interfaces;

namespace StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Modules
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserReadRepository>().As<IUserReadRepository>();
        }
    }
}
