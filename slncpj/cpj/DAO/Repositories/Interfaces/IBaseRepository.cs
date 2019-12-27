using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity Find(int id);

        void Remove(int id);

        void Update(TEntity entity);
    }
}
