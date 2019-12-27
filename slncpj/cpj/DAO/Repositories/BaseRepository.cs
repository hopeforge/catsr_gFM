using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly CPJContext _contexto;

        public BaseRepository(CPJContext context)
        {
            _contexto = context;
        }

       
        public void Add(TEntity entity)
        {
            _contexto.Set<TEntity>().Add(entity);
            _contexto.SaveChanges();
        }

        public TEntity Find(int id)
        {
            return _contexto.Set<TEntity>().Find(id);

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _contexto.Set<TEntity>().ToList();
        }

        public void Remove(int id)
        {
            var entity = _contexto.Set<TEntity>().Find(id);
            _contexto.Set<TEntity>().Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _contexto.Set<TEntity>().Update(entity);
            _contexto.SaveChanges();
        }
    }
}
