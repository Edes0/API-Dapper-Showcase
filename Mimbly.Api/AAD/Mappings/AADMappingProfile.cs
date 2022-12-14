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
                    inp => inp.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<CreateCompanyDTO, CompanyModel>()
            .ForMember(dest => dest.Name, inp => inp.MapFrom(src => src.CompanyName));
    }
}
