using Note.Entities;

namespace Note.Data.Repository
{
    public interface IBookRepository : IEntityRepository<Book>
    {
        Book? GetByTitleAndAuthor(string? title, string? authorName);
    }

    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public BookRepository(NoteDataContext noteDataContext) : base(noteDataContext)
        {
            
        }

        public Book? GetByTitleAndAuthor(string? title, string? authorName)
        {
            var allBooks = _context.GetAll<Book>();
            if (allBooks?.Any() != true)
                return null;

            return allBooks
                .Where(b => b.AuthorName!.ToLower().Equals(authorName!.ToLower()))
                .FirstOrDefault();
        }
    }

}
