namespace Note.Entities;

public class Character : Entity
{
    public string? Name { get; set; }
    public long BookId { get; set; }

    public override string ToRedisString(int id)
    {
        return $"{id}-{BookId}-{Name}";
    }
}
