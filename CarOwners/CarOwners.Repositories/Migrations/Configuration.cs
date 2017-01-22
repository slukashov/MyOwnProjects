using CarOwners.Repositories.Contexts;
using System.Data.Entity.Migrations;

namespace CarOwners.Repositories.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
        }
    }
}
