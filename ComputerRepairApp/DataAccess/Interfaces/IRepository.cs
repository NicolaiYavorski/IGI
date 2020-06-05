using System.Linq;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey> 
    {
        TKey Create(TEntity item);

        void Update(TEntity item);

        void Delete(TKey id);

        IQueryable<TEntity> GetAll();

        TEntity GetById(TKey id);
    }
}
