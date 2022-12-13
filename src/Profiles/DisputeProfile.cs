using AutoMapper;
using src.Entities;
using src.Models.Dtos;

namespace src.Profiles
{
    public class DisputeProfile : Profile
    {
        public DisputeProfile()
        {
            CreateMap<DisputeForCreationDto, Dispute>();
            CreateMap<Dispute, DisputeForCreationDto>();
            CreateMap<Dispute, DisputeForDisplayForLawyerDto>();
            CreateMap<Dispute, DisputeForDisplayForCustomerDto>();
        }
    }
}
