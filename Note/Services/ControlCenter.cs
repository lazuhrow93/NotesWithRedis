using AutoMapper;
using Note.App.Controllers.Library.Dto;
using Note.Data.Repository;
using Note.Domain;
using Note.Domain.EntityModel;
using Note.Entities;

namespace Note.App.Services
{
    public interface IControlCenter
    {
        void AddBook(BookDto bookDto);
    }

    public class ControlCenter : IControlCenter
    {
        private IBookRepository _bookRepository;
        private ICharacterRepostiory _characterRepostiory;
        private IMapper _mapper;
        private IEntitySync _entitySync;

        public ControlCenter(
            IBookRepository bookRepository,
            ICharacterRepostiory characterRepository,
            IEntitySync entitySync,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _characterRepostiory = characterRepository;
            _entitySync = entitySync;
            _mapper = mapper;
        }

        public void AddBook(BookDto bookDto)
        {
            var model = _mapper.Map<BookDto, BookModel>(bookDto);
            var entity = _bookRepository.GetByTitleAndAuthor(model.Title, model.AuthorName);
            _entitySync.Sync(entity, model);
        }

        public void AddCharacter(CharacterDto characterDto)
        {
            var entity = _mapper.Map<CharacterDto, Character>(characterDto);
            _characterRepostiory.Add(entity);
        }

    }

    public class DomainMapper : Profile
    {
        public DomainMapper()
        {
            CreateMap<BookDto, Book>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName));

            CreateMap<CharacterDto, Character>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.BookId, opt => opt.Ignore());
        }
    }
}
