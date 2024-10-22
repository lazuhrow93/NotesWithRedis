namespace Note.Entities;

public class Character : Entity
{
    public string? Name { get; set; }
    public long BookId { get; set; }

    public override string ToRedisString()
    {
        return $"{Id}-{BookId}-{Name}";
    }
}
