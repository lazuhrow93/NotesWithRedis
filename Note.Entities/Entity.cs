namespace Note.Entities;

public abstract class Entity
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public abstract string ToRedisString();
}
