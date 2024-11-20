using Note.Entities;

namespace Note.Data
{
    public interface INoteDataContext
    {
        bool Add<T>(T entity) where T : Entity;
        T[]? GetAll<T>();
    }
}

