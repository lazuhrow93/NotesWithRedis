using Note.Data.RedisLibrary;
using Note.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Note.Data
{
    public interface INoteDataContext
    {
        bool Add<T>(T entity) where T : Entity;
        T[]? GetAll<T>();
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
 
        public T[]? GetAll<T>()
        {
            var modelKey = _keyProvider.Model<T>();
            var result = _database.StringGet(modelKey).ToString();

            
            return [];
        }

        private long GetNextId<T>()
        {
            var keyForIdentifier = _keyProvider.ForIdentifier();
            var rawRedisValue = _database.StringGet(keyForIdentifier);
            var idObj = SerializeFromRedisValue<Identifier>(rawRedisValue);

            if (idObj == null)
                throw new Exception("Identifier object not set");

            switch (typeof(T).Name)
            {
                case "Book" :
                    return idObj.BookId;
                case "Character":
                    return idObj.CharacterId;
                case "Note":
                    return idObj.NoteId;
                default:
                    throw new Exception("That entity is not configured to have an Id");
            }
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
}

