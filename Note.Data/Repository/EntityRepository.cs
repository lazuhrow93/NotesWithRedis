using Note.Entities;
using StackExchange.Redis;
using System;

namespace Note.Data.Repository
{
    public interface IEntityRepository<TEntity>
    {
        TEntity[]? GetAll();
        bool Add(TEntity entity);
        bool Remove(TEntity entity);
        bool Update(TEntity entity);
    }

    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        protected INoteDataContext _context;

        public EntityRepository(INoteDataContext database)
        {
            _context = database;
        }

        public TEntity[]? GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Add(TEntity entity)
        {
            return _context.Add(entity);
        }

        public bool Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entit)
        {
            throw new NotImplementedException();
        }
    }
}
