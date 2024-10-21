using Note.Entities;
using StackExchange.Redis;
using System.Formats.Tar;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Note.Data
{
    public interface INoteDataContext
    {
        Book[]? GetAllBooks();
        bool AddBook(Book book);
    }

    public class NoteDataContext : INoteDataContext
    {
        private static int _bookId = 0;
        
        private IDatabase _database;

        public NoteDataContext(IDatabase database)
        {
            _database = database;    
        }

        public bool AddBook(Book book)
        {
            var redisKey = new RedisKey("book");
            var redisValue = new RedisValue(book.ToRedisString(++_bookId));
            var result = _database.SetAdd(redisKey, redisValue);
            return true;
        }

        public Book[]? GetAllBooks()
        {
            var redisKey = new RedisKey("books");
            var s = _database.StringGet(redisKey); //an array of [id-bookTitle]
            return SerializeFromRedisValue<Book[]>(s);
        }

        private T? SerializeFromRedisValue<T>(RedisValue redisValue)
        {
            var x = redisValue.ToString();
            return JsonSerializer.Deserialize<T>(x);
        }
    }
}

