using System.Data.Entity;
using CarOwners.Entities.Entities;

namespace CarOwners.Repositories.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("CarOwnersContext")
        {
        }

        public IDbSet<CarOwner> CarOwners { get; set; }
        public IDbSet<Car> Car { get; set; }
    }
}
