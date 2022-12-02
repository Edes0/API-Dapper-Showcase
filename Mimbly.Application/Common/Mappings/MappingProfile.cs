namespace Mimbly.Application.Common.Mappings;

using AutoMapper;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mimbox mapping
        CreateMap<Mimbox, MimboxDto>();

        CreateMap<CreateMimboxRequestDto, Mimbox>();

        CreateMap<UpdateMimboxRequestDto, Mimbox>();

        // Mimbox Location
        CreateMap<MimboxLocation, MimboxLocationDto>();

        CreateMap<CreateMimboxLocationRequestDto, MimboxLocation>();

        CreateMap<UpdateMimboxLocationRequestDto, MimboxLocation>();

        // Company mapping
        CreateMap<Company, CompanyDto>();

        CreateMap<CreateCompanyRequestDto, Company>();

        CreateMap<UpdateCompanyRequestDto, Company>();

        // CompanyContact mapping
        CreateMap<CompanyContact, CompanyContactDto>();

        CreateMap<CreateCompanyContactRequestDto, CompanyContact>();

        CreateMap<UpdateCompanyContactRequestDto, CompanyContact>();
    }
}

