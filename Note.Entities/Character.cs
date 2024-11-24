namespace Note.Entities;

public class Character : Entity
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Name { get; set; }
    public long BookId { get; set; }

    public override string ToRedisString()
    {
        return $"{Id}-{BookId}-{Name}";
    }
}
