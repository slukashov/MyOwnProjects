using System.Data.Entity;
using CarOwners.Entities.Entities;
using Ninject;

namespace CarOwners.Repositories.Contexts
{
    public class DatabaseContext : DbContext
    {
        [Inject]
        public DatabaseContext() : base("CarOwnersContext")
        {
        }

        public IDbSet<CarOwner> CarOwners { get; set; }
        public IDbSet<Car> Car { get; set; }
    }
}
