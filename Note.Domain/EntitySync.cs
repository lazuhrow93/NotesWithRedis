using AutoMapper;
using Note.Entities;

namespace Note.Domain
{
    public interface IEntitySync
    {
        TEntity Sync<TEntity, TEntityModel>(TEntity? entity, TEntityModel model)
            where TEntity : Entity;
    }

    public class EntitySync : IEntitySync
    {
        private IMapper _mapper;

        public EntitySync(IMapper mapper)
        {
            _mapper = mapper;    
        }
        public TEntity Sync<TEntity, TEntityModel>(TEntity? entity, TEntityModel model)
            where TEntity : Entity
        {
            if(entity is null)
                return _mapper.Map<TEntityModel, TEntity>(model);
            else
                return _mapper.Map<TEntityModel, TEntity>(model, entity);
        }
    }
}
