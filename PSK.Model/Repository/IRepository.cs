using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        List<TEntity> Get();
        TEntity Get(int id);
        TEntity Delete(int id);
        TEntity Update(TEntity entity);
    }
}
