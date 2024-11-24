namespace Note.App.Controllers.Library.Requests
{
    public class AddCharacterToCatalogRequest
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? BookName { get; set; }
        public string? AuthorName { get; set; }
    }
}
