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
                    inp => inp.MapFrom(src => $"{src.FirstName}{src.LastName}"))
                    .ForMember(
                    dest => dest.EmailAddress, inp => inp.MapFrom( src => src.Email
                    )).ForMember(
                    dest => dest.MobilePhone, inp => inp.MapFrom(src => src.Phone
                    ));

    }
}
