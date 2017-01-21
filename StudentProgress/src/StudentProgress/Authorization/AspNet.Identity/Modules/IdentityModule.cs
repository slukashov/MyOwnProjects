using Autofac;
using StudentProgress.Context;

namespace StudentProgress.Authorization.AspNet.Identity.Modules
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<StudentProgressContext>();
        }
    }
}
