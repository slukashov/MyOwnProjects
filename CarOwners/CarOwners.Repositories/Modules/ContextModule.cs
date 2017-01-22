using System.Data.Entity;
using CarOwners.Repositories.Contexts;
using Ninject.Modules;

namespace CarOwners.Repositories.Modules
{
    public class ContextModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DatabaseContext>();
        }
    }
}