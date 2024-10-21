namespace Note.Entities;

public class Book : Entity
{
    public string? Title { get; set; }

    public override string ToRedisString(int id)
    {
        return $"{id}-{Title!.ToLower()}";
    }
}
