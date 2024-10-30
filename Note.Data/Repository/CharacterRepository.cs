using Note.Entities;
using Entity = Note.Entities;

namespace Note.Data.Repository;

public interface ICharacterRepostiory : IEntityRepository<Character>
{
    Character? GetByBook(long bookId, string characterName);
}

public class CharacterRepository : EntityRepository<Character>, ICharacterRepostiory
{
    public CharacterRepository(NoteDataContext noteDataContext) : base(noteDataContext)
    {
        
    }

    public Character? GetByBook(long bookId, string characterName)
        => _context.GetAll<Character>()?.Where(c => c.BookId == bookId && c.Name == characterName).FirstOrDefault();

}
