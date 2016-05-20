using ProAsp.Data.DatabaseContext;
using ProAsp.Data.Models;

namespace ProAsp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProAspDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            CommandTimeout = Int32.MaxValue;
        }

        protected override void Seed(ProAspDbContext context)
        {
            

            context.Users.AddOrUpdate(u=>u.Name, new User{Id = Guid.NewGuid(), Name = "Adam"});
        }
    }
}
