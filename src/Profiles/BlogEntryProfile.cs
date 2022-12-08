using AutoMapper;
using src.Entities;
using src.Models.Dtos;

namespace src.Profiles
{
    public class BlogEntryProfile:Profile
    {
        public BlogEntryProfile()
        {
            CreateMap<BlogEntry, BlogEntryForDisplayDto>();
            CreateMap<BlogEntryForCreationDto, BlogEntry>();
        }
    }
}
