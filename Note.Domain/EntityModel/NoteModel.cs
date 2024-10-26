namespace Note.Domain.EntityModel
{
    public class NoteModel
    {
        public string? TextValue { get; set; }
        public long BookId { get; set; }
        public long CharacterId { get; set; }
        public int ChapterNumber { get; set; }
    }
}
