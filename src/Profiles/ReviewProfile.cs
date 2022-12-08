using AutoMapper;

namespace EarlyMan.PL.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<src.Entities.Review, src.Models.Dtos.ReviewForDisplayDto>()
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src =>
                (src.UpdatedAt == new DateTime()) ? src.TimeStamp : src.UpdatedAt));

            CreateMap<src.Models.Dtos.ReviewForCreationDto, src.Entities.Review>()
                .ForMember(dest => dest.ReviewId, o => o.MapFrom(guid => Guid.NewGuid()));
            CreateMap<src.Models.Dtos.ReviewForCreationDto, src.Entities.Review>()
                .ForMember(dest => dest.UserId, o => o.MapFrom(guid => Guid.Empty));
            CreateMap<src.Models.Dtos.ReviewForCreationDto, src.Entities.Review>()
                .ForMember(dest => dest.TimeStamp, o => o.MapFrom(time => DateTime.Now));
            CreateMap<src.Entities.Review, src.Models.Dtos.ReviewForUpdateDTO>();
            CreateMap<src.Models.Dtos.ReviewForUpdateDTO, src.Entities.Review>();

            CreateMap<src.Entities.Review, src.Models.Dtos.UpdatedRequestDTO>().ReverseMap()
                .ForMember(x => x.ViewLastTime, opt => opt.MapFrom(time => time.UpdatedAt));
        }
    }
}