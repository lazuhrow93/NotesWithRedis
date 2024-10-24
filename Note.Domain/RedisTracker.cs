using Note.Entities;

namespace Note.Domain.RedisLibrary
{
    public enum EntityState
    {
        NoChange,
        Updated,
        Added,
        Deleted
    }

    public class RedisTracker<T>
        where T : Entity
    {
        public T? Entity { get; set; }
        public EntityState? State { get; set; }

        public bool Added()
            => State == EntityState.Added;

        public bool Updated()
            => State == EntityState.Updated;

        public bool Deleted()
            => State == EntityState.Deleted;

        public bool NoChange()
            => State == EntityState.NoChange;

        public RedisTracker(T Value, EntityState state)
        {
            Entity = Value;
            State = state;
        }

        public static RedisTracker<T> Add(T Entity)
            => new(Entity, EntityState.Added);

        public static RedisTracker<T> Updated(T Entity)
            => new(Entity, EntityState.Updated);

        public static RedisTracker<T> NoChange(T Entity)
            => new(Entity, EntityState.NoChange);

        public static RedisTracker<T> Deleted(T Entity)
            => new(Entity, EntityState.Deleted);
    }
}
