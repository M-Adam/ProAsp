using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data.Models;

namespace ProAsp.Data
{
    public class GenericRepository<T>:IRepository<T> where T:BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public GenericRepository(IDbContext context)
        {
            this._context = context;
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public virtual IQueryable<T> Table
        {
            get { return this.Entities; }
        }

        public void Insert(T entity)
        {
            if(entity==null)
                throw new ArgumentNullException("entityInsert");
            this.Entities.Add(entity);
            this._context.SaveChanges();
        }

        public void Update(T entity)
        {
            if(entity==null)
                throw new ArgumentNullException("entityUpdate");

            this._context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entityDelete");

            this.Entities.Remove(entity);
            this._context.SaveChanges();
        }

        public T GetById(Guid id)
        {
            return this.Entities.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> whereExpr)
        {
            return this.Entities.Where(whereExpr);
        }

        public T GetSingle(Expression<Func<T, bool>> whereExpr)
        {
            return this.Entities.Single(whereExpr);
        }
    }
}
