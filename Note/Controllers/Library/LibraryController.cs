using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.App.Controllers.Library.Dto;
using Note.App.Controllers.Library.Responses;
using Note.Data.Repository;
using Note.Entities;

namespace Note.App.Controllers.Library
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController
    {
        private ILogger<LibraryController> _logger;
        private IBookRepository _bookRepository;
        private ICharacterRepostiory _characterRepository;
        private IMapper _mapper;

        public LibraryController(ILogger<LibraryController> logger,
            IBookRepository bookRepository,
            ICharacterRepostiory characterRepostiory,
            IMapper mapper)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _characterRepository = characterRepostiory;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("books")]
        public GetBooksCatalogResponse GetBooksCatalog()
        {
            var books = _bookRepository.GetAll();
            var response = new GetBooksCatalogResponse()
            {
                BookDtos = _mapper.Map<BookDto[]>(books)
            };

            return response;
        }

        [HttpGet]
        [Route("addbooks")]
        public bool AddBooksCatalog()
        {
            return _bookRepository.Add(new Book()
            {
                Title = $"The Eye of the World"
            });
        }

        [HttpGet]
        [Route("AddCharacter")]
        public bool AddCharacterCatalog()
        {
            var dateTime = DateTime.UtcNow;
            return _characterRepository.Add(new Character()
            {
                Name = "Egwnene Al' Vere",
                BookId = 1
            });
        }

        public class LibraryProfile : Profile
        {
            public LibraryProfile()
            {
                CreateMap<Book, BookDto>()
                    .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                    .ForMember(d => d.BookId, opt => opt.MapFrom(s => s.Id));
            }
        }
    }
}
