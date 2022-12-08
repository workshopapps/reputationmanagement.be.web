using AutoMapper;

namespace EarlyMan.PL.Profiles
{
    public class UserAccountProfile:Profile
    {
        public UserAccountProfile()
        {
            CreateMap<src.Models.Dtos.CustomerAccountForCreationDto, src.Entities.ApplicationUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(source => source.BusinessEntityName.Replace(" ", "_")))
                .ForMember(x=>x.PostAddress, o => o.MapFrom(str => string.Empty));
            CreateMap<src.Models.Dtos.LawyerAccountForCreationDto, src.Entities.ApplicationUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(source => source.FirstName + source.LastName))
                .ForMember(x => x.PostAddress, o => o.MapFrom(str => string.Empty));

            CreateMap<src.Entities.ApplicationUser, src.Models.Dtos.UserDetailsDto>()
            .ForMember(x => x.BusinessEntityName, opt => opt.MapFrom(source => source.UserName.Replace("_", " ")));

            CreateMap<src.Models.AccessibilityOptions, src.Entities.ApplicationUser>();
            CreateMap<src.Entities.ApplicationUser, src.Models.AccessibilityOptions>();

            CreateMap<src.Entities.ApplicationUser, src.Models.Dtos.UpdateNotificationForUserDto>();
            CreateMap<src.Models.Dtos.UpdateNotificationForUserDto, src.Entities.ApplicationUser>();

        }
    }
}
