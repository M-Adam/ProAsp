using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using ProAsp.Data.Models;

namespace ProAsp.Data.DatabaseContext
{
    public class ProAspDbContext : DbContext, IDbContext
    {
        public ProAspDbContext() : base("ProAspDb")
        {
            Database.SetInitializer<ProAspDbContext>(new MigrateDatabaseToLatestVersion<ProAspDbContext,ProAsp.Data.Migrations.Configuration>());
        }

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

            modelBuilder.Entity<Article>().HasOptional(x => x.Creator).WithMany(x=>x.Articles);
            modelBuilder.Entity<User>().HasMany(x => x.Articles).WithOptional(x => x.Creator);
            

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Article> Articles { get; set; }

        
    }
}
