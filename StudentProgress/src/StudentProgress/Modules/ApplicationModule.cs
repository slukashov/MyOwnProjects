using Autofac;
using StudentProgress.Infrastructure.Wrappers;

namespace StudentProgress.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SignInManegerWrapper>();
        }
    }
}
    