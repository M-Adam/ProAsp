using System.Data.Entity;
using ProAsp.Data.Models;

namespace ProAsp.Data.DatabaseContext
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
