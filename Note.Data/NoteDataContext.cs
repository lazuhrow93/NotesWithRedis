using Note.Data.RedisLibrary;
using Note.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Note.Data
{
    public class NoteDataContext : INoteDataContext
    {
        private static int _bookId = 0;
        
        private IDatabase _database;
        private IRedisKeyProvider _keyProvider;

        public NoteDataContext(IDatabase database,
            IRedisKeyProvider redisKeyProvider)
        {
            _database = database;    
            _keyProvider = redisKeyProvider;
        }

        public bool Add<T>(T entity)
            where T : Entity
        {
            var reservedId = ReserveId<T>();
            var modelKey = _keyProvider.Entity<T>();
            entity.Id = reservedId;
            var newEntity = new RedisValue(entity.ToRedisString());
            var result = _database.SetAdd(modelKey, newEntity);
            return result;
        }
 
        public T[]? GetAll<T>()
        {
            var modelKey = _keyProvider.Entity<T>();
            var result = _database.StringGet(modelKey).ToString();
            
            return [];
        }

        private T? SerializeFromRedisValue<T>(RedisValue redisValue)
        {
            var x = redisValue.ToString();
            return JsonSerializer.Deserialize<T>(x);
        }

        #region Private Helpers

        private long ReserveId<T>()
        {
            var redisKey = _keyProvider.Identifier<T>();
            var currentValue = _database.HashIncrement(redisKey, new RedisValue(typeof(T).Name), 1);
            return currentValue;
        }

        #endregion
    }
}

