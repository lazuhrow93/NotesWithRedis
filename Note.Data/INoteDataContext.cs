using Note.Entities;
using StackExchange.Redis;
using System.Formats.Tar;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Note.Data
{
    public interface INoteDataContext
    {
        bool Add<T>(T entity) where T : Entity;
    }

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
            var modelKey = _keyProvider.Model<T>();
            entity.Id = reservedId;
            var result = _database.SetAdd(modelKey, new RedisValue(entity.ToRedisString()));
            return result;
        }

        private T? SerializeFromRedisValue<T>(RedisValue redisValue)
        {
            var x = redisValue.ToString();
            return JsonSerializer.Deserialize<T>(x);
        }

        private long ReserveId<T>()
        {
            var redisKey = _keyProvider.Identifier<T>();
            return _database.StringIncrement(redisKey, 1);
        }
    }

    public interface IRedisKeyProvider
    {
        RedisKey Identifier<T>();
        RedisKey Model<T>();
    }

    public class RedisKeyProvider : IRedisKeyProvider
    {
        private const string _delimiter = ":";

        public RedisKey Identifier<T>()
        {
            return new RedisKey($"identifier{_delimiter}{nameof(T)}");
        }

        public RedisKey Model<T>()
        {
            return new RedisKey($"{nameof(T)}");
        }
    }
}

