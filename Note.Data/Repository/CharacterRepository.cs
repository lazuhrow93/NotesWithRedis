using Note.Entities;

namespace Note.Data.Repository;

public interface ICharacterRepostiory : IEntityRepository<Character>
{

}

public class CharacterRepository : EntityRepository<Character>, ICharacterRepostiory
{
    public CharacterRepository(NoteDataContext noteDataContext) : base(noteDataContext)
    {
        
    }
}
