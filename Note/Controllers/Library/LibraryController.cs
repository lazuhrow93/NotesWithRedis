using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.App.Controllers.Library.Dto;
using Note.App.Controllers.Library.Requests;
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
        [Route("addbooks")]
        public bool AddBooksCatalog(AddBookToCatalogRequests request)
        {
            var details = _mapper.Map<BookDto>(request);
            return _bookRepository.Add(details);
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
                CreateMap<AddBookToCatalogRequests, BookDto>()
                    .ForMember(d => d.Title, opt => opt.MapFrom(s => s.BookName))
                    .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName));
            }
        }
    }
}
