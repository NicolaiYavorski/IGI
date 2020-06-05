using DataAccess.EfContext;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repositories
{
    internal class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public BaseRepository(ComputerContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        protected ComputerContext Context { get; set; }

        protected DbSet<TEntity> DbSet { get; }

        public virtual TKey Create(TEntity item)
        {
            var result = DbSet.Add(item);
            Context.SaveChanges();
            return result.Entity.Id;
        }

        public virtual void Update(TEntity item)
        {
            Context.Entry(DbSet.Find(item.Id)).State = EntityState.Detached;
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }
    }
}
