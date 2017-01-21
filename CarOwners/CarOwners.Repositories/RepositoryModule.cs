using System.Data.Entity;
using CarOwners.Entities.Entities;
using CarOwners.Repositories.Contexts;
using CarOwners.Repositories.Repositories;
using CarOwners.Repositories.Repositories.Interfaces;
using Ninject.Modules;

namespace CarOwners.Repositories
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGenericRepository<Car>>().To<GenericRepository<Car>>();
            Bind<IGenericRepository<CarOwner>>().To<GenericRepository<CarOwner>>();
            Bind<DbContext>().To<DatabaseContext>();
        }
    }
}
