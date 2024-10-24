using AutoMapper;
using Note.Domain.RedisLibrary;
using Note.Entities;

namespace Note.Domain;

public interface IEntitySync
{
    RedisTracker<TEntity> Sync<TEntity, TModel>(TEntity? entity, TModel model)
        where TEntity : Entity;
}

public class EntitySync : IEntitySync
{
    private IMapper _mapper;

    public EntitySync(IMapper mapper)
    {
        _mapper = mapper;    
    }
    public RedisTracker<TEntity> Sync<TEntity, TModel>(TEntity? entity, TModel model)
        where TEntity : Entity
    {
        if (entity is null)
            return Create<TEntity, TModel>(model);

        return RedisTracker<TEntity>.Updated(_mapper.Map(model, entity)); //improvement: check if it was actually an update
    }

    private RedisTracker<TEntity> Create<TEntity, TModel>(TModel Model)
        where TEntity : Entity
    {
        return RedisTracker<TEntity>.Add(_mapper.Map<TEntity>(Model));
    }
}
