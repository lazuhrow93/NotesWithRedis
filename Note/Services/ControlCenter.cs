using AutoMapper;
using Note.App.Controllers.Library.Dto;
using Note.Data.Repository;
using Note.Domain;
using Note.Domain.EntityModel;
using Note.Entities;
using Entity = Note.Entities;

namespace Note.App.Services
{
    public interface IControlCenter
    {
        void AddBook(BookDto bookDto);
        void AddCharacter(CharacterDto characterDto);
    }

    public class ControlCenter : IControlCenter
    {
        private IBookRepository _bookRepository;
        private ICharacterRepostiory _characterRepository;
        private INoteRepository _noteRepository;
        private IMapper _mapper;
        private IEntitySync _entitySync;

        public ControlCenter(
            IBookRepository bookRepository,
            ICharacterRepostiory characterRepository,
            INoteRepository noteRepository,
            IEntitySync entitySync,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _characterRepository = characterRepository;
            _noteRepository = noteRepository;
            _entitySync = entitySync;
            _mapper = mapper;
        }

        public void AddBook(BookDto bookDto)
        {
            var model = _mapper.Map<BookDto, BookModel>(bookDto);

            var entity = _bookRepository.GetByTitleAndAuthor(model.Title, model.AuthorName);
            var redisTracker = _entitySync.Sync(entity, model);

            if (redisTracker.Added())
            {
                _bookRepository.Add(redisTracker.Entity!);
                return;
            }
        }

        public void AddCharacter(CharacterDto characterDto)
        {
            var characterModel = _mapper.Map<CharacterDto, CharacterModel>(characterDto);
            var book = _bookRepository.GetByTitleAndAuthor(characterDto.BookName, characterDto.AuthorName);
            if (book == null)
                throw new Exception($"Book doesn't exist");

            var existingCharacter = _characterRepository.GetByBook(book.Id, characterDto.Name!);
            var redisTracker = _entitySync.Sync(existingCharacter, characterModel);

            if (redisTracker.Added())
            {
                _characterRepository.Add(redisTracker.Entity!);
                return;
            }
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
                .ForMember(d=>d.FirstName, opt => opt.MapFrom(s=>s.FirstName))
                .ForMember(d=>d.MiddleName, opt => opt.MapFrom(s=>s.MiddleName))
                .ForMember(d=>d.LastName, opt => opt.MapFrom(s=>s.LastName))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.BookId, opt => opt.Ignore());

            CreateMap<BookDto, BookModel>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName));
        }
    }
}
