using Note.Entities;
using StackExchange.Redis;

namespace Note.Data.Repository
{
    public interface IEntityRepository<TEntity>
    {
        TEntity[]? GetAll();
    }

    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        private INoteDataContext _context;

        public EntityRepository(INoteDataContext database)
        {
            _context = database;
        }

        public TEntity[]? GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
