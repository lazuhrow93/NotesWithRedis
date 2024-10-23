using Note.Entities;

namespace Note.Domain
{
    public interface IEntitySync
    {
        bool Sync<TEntity, TEntityModel>(TEntity entity, TEntityModel model);
    }

    public class EntitySync : IEntitySync
    {
        public bool Sync<TEntity, TEntityModel>(TEntity entity, TEntityModel model)
            where TEntity : Entity
        {

        }
    }
}
