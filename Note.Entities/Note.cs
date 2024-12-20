﻿namespace Note.Entities;

public class Note : Entity
{
    public string? TextValue { get; set; }
    public long BookId { get; set; }
    public int ChapterNumber { get; set; }
    public long CharacterId { get; set; }

    public override string ToRedisString()
    {
        return $"{Id}-{BookId}-{ChapterNumber}-{CharacterId}-{TextValue}";
    }
}
