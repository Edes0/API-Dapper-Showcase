namespace Mimbly.Application.Common.Mappings;

using AutoMapper;
using Contracts.Dtos.AD;
using Domain.Entities.AD;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Contracts.Dtos.EventLog;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Application.Contracts.Dtos.MimboxErrorLog;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // AD
        CreateMap<InviteUserDto, AdUser>()
            .ForMember(
                dest => dest.DisplayName,
                inp => inp.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<AddCompanyDto, AdCompany>()
            .ForMember(dest => dest.Name, inp => inp.MapFrom(src => src.CompanyName));

        // Mimbox
        CreateMap<Mimbox, MimboxDto>();

        CreateMap<CreateMimboxRequestDto, Mimbox>();

        CreateMap<UpdateMimboxRequestDto, Mimbox>();

        // MimboxErrorLog
        CreateMap<UpdateMimboxErrorLogRequestDto, MimboxErrorLog>();

        // MimboxLocation
        CreateMap<MimboxLocation, MimboxLocationDto>();

        CreateMap<CreateMimboxLocationRequestDto, MimboxLocation>();

        CreateMap<UpdateMimboxLocationRequestDto, MimboxLocation>();

        // Company
        CreateMap<Company, CompanyDto>();

        CreateMap<CreateCompanyRequestDto, Company>();

        CreateMap<UpdateCompanyRequestDto, Company>();

        // CompanyContact
        CreateMap<CompanyContact, CompanyContactDto>();

        CreateMap<CreateCompanyContactRequestDto, CompanyContact>();

        CreateMap<UpdateCompanyContactRequestDto, CompanyContact>();
    }
}

