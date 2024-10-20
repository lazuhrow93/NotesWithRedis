using StackExchange.Redis;
using System.Formats.Tar;
using System.Text.Json;

namespace Note.Data
{
    public interface INoteDataContext
    {
        public T Add<T>(T Entity, string user);
        public T Update<T>(T Entity, string user);
        public bool Delete<T>(T Entity, string user);
    }

    public class NoteDataContext : INoteDataContext
    {
        private IDatabase _database;

        public NoteDataContext(IDatabase database)
        {
            _database = database;    
        }

        public T Add<T>(T entity, string user)
        {
            var redisKey = new RedisKey($"{user}:{nameof(T)}");
            var redisValue = new RedisValue(entity.ToRedisValue());
            _database.SetAdd(redisKey, redisValue);
            return entity;
        }

        public T Update<T>(T Entity, string user)
        {
            var redisKey = new RedisKey($"{user}:{nameof(T)}");
            var obj = _database.StringGet(redisKey);
            if(obj == RedisValue.Null)
                throw new Exception();

            var objDeserialize = JsonSerializer.Deserialize<T>(obj!.ToString());
            objDeserialize = Entity;
            var newObj = JsonSerializer.Serialize(objDeserialize);

            _database.SetAdd(redisKey, newObj);

            return objDeserialize;
        }

        public bool Delete<T>(T Entity, string user)
        {
            var redisKEy = new RedisKey($"{user}:{nameof(T)}");
            return _database.KeyDelete(redisKEy);
        }
    }
}

