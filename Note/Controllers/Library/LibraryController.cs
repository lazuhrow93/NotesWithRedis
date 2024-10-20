﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Note.App.Controllers.Library.Dto;
using Note.App.Controllers.Library.Responses;
using Note.Data.Repository;
using Note.Entities;

namespace Note.App.Controllers.Library
{
    [ApiController]
    public class LibraryController
    {
        private ILogger<LibraryController> _logger;
        private IBookRepository _bookRepository;
        private IMapper _mapper;

        public LibraryController(ILogger<LibraryController> logger,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "books")]
        public GetBooksCatalogResponse GetBooksCatalog()
        {
            var books = _bookRepository.GetAll();
            var response = new GetBooksCatalogResponse()
            {
                BookDtos = _mapper.Map<BookDto[]>(books)
            };

            return response
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
