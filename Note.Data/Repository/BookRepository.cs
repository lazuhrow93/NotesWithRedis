using Note.Entities;

namespace Note.Data.Repository
{
    public interface IBookRepository : IEntityRepository<Book>
    {
    }

    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public BookRepository(NoteDataContext noteDataContext) : base(noteDataContext)
        {
            
        }
    }

}
