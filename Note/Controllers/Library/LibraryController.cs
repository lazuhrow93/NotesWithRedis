﻿using AutoMapper;
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

        [HttpPost]
        [Route("addbooks")]
        public bool AddBooksCatalog(AddBookToCatalogRequest request)
        {
            var bookDetails = _mapper.Map<BookDto>(request);
            _controlCenter.AddBook(bookDetails);
            return true;
        }

        [HttpPost]
        [Route("AddCharacter")]
        public bool AddCharacterCatalog(AddCharacterToCatalogRequest request)
        {
            var characterDetails = _mapper.Map<CharacterDto>(request);
            _controlCenter.AddCharacter(characterDetails);
            return true;
        }

        public class LibraryProfile : Profile
        {
            public LibraryProfile()
            {
                CreateMap<AddBookToCatalogRequest, BookDto>()
                    .ForMember(d => d.Title, opt => opt.MapFrom(s => s.BookName))
                    .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName));

                CreateMap<AddCharacterToCatalogRequest, CharacterDto>()
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                    .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                    .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.BookName, opt => opt.MapFrom(s => s.BookName))
                    .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName));

            }
        }
    }
}
