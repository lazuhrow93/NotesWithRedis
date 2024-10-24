using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.App.Controllers.Library.Dto;
using Note.App.Controllers.Library.Requests;
using Note.App.Controllers.Library.Responses;
using Note.App.Services;
using Note.Data.Repository;
using Note.Entities;

namespace Note.App.Controllers.Library
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController
    {
        private ILogger<LibraryController> _logger;
        private IMapper _mapper;
        private IControlCenter _controlCenter;

        public LibraryController(ILogger<LibraryController> logger,
            IMapper mapper,
            IControlCenter controlCenter)
        {
            _logger = logger;
            _mapper = mapper;
            _controlCenter = controlCenter;
        }

        [HttpGet]
        [Route("addbooks")]
        public bool AddBooksCatalog(AddBookToCatalogRequests request)
        {
            var bookDetails = _mapper.Map<BookDto>(request);
            _controlCenter.AddBook(bookDetails);
            return true;
        }

        [HttpGet]
        [Route("AddCharacter")]
        public bool AddCharacterCatalog()
        {
            var dateTime = DateTime.UtcNow;
            //return _characterRepository.Add(new Character()
            //{
            //    Name = "Egwnene Al' Vere",
            //    BookId = 1
            //});
            return true;
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
