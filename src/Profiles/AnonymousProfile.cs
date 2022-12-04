using AutoMapper;

namespace src.Profiles
{
    public class AnonymousProfile : Profile
    {
        public AnonymousProfile() 
        {
            CreateMap<src.Entities.CareerResponse, src.Models.Dtos.CareerResponseDto>();
        }
    }
}
