using AutoMapper;
using Note.Domain.EntityModel;
using Note.Entities;

namespace Note.Domain
{
    public class EntitySyncProfile : Profile
    {
        public EntitySyncProfile()
        {
            CreateMap<BookModel, Book>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.AuthorName))
                .ForMember(d => d.AuthorId, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.CreatedBy, opt => opt.Ignore());
        }
    }
}
