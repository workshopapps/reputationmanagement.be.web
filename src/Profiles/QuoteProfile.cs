using AutoMapper;
using src.Entities;
using src.Models.Dtos;

namespace src.Profiles
{
    public class QuoteProfile:Profile
    {
        public QuoteProfile()
        {
            CreateMap<QuoteForCreationDto, Quote>();
            CreateMap<Quote, QuoteForCreationDto>();

            CreateMap<QuoteForCreationFromBlogDto, Quote>();
            CreateMap<Quote, QuoteForCreationFromBlogDto>();
        }
    }
}
