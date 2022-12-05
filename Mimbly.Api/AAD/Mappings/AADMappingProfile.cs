namespace Mimbly.Api.AAD.Mappings;

using AutoMapper;
using Mimbly.Api.AAD.DTOs;

public class AADMappingProfile : Profile
{
    public AADMappingProfile()
    {
        CreateMap<UserInviteDTO, InvitedUser>()
            .ForMember(
            dest => dest.DisplayName,
            dest => dest.MapFrom(src => string.Format("{1}{2}", src.FirstName, src.LastName
            )))
            .ForMember(dest => dest.Contact.JobTitle, dest => dest.MapFrom(src => src.JobTitle))
            .ForMember(dest => dest.Contact.MobilePhone, dest => dest.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Contact.StreetAddress, dest => dest.MapFrom(src => src.StreetAddress))
            .ForMember(dest => dest.Contact.City, dest => dest.MapFrom(src => src.City))
            .ForMember(dest => dest.Contact.Country, dest => dest.MapFrom(src => src.Country));
    }
}
