using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProAsp.Data.Repository
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> whereExpr);
        
        T GetSingle(Expression<Func<T, bool>> whereExpr);
    }
}
