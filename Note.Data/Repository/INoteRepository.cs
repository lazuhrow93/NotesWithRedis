using Note.Entities;

using Entity = Note.Entities;

namespace Note.Data.Repository;

public interface INoteRepository : IEntityRepository<Entity.Note>
{
}

public class NoteRepository : EntityRepository<Entity.Note>, INoteRepository
{
    public NoteRepository(NoteDataContext database) : base(database)
    {
    }
}
