using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data.Migrations;
using ProAsp.Data.Models;

namespace ProAsp.Data
{
    public class ProAspDbContext : DbContext, IDbContext
    {
        public ProAspDbContext() : base("ProAspDb")
        {
            Database.SetInitializer<ProAspDbContext>(new MigrateDatabaseToLatestVersion<ProAspDbContext,Configuration>());
        }

        public DbSet<User> Users { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => !string.IsNullOrEmpty(x.Namespace))
                    .Where(
                        x =>
                            x.BaseType != null && x.BaseType.IsGenericType &&
                            x.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (Type type in typesToRegister)
            {
                dynamic configInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
