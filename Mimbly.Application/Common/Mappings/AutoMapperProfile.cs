namespace Mimbly.Application.Common.Mappings;

using AutoMapper;
using Contracts.Dtos.AD;
using Domain.Entities.AD;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Application.Contracts.Dtos.MimboxContact;
using Mimbly.Application.Contracts.Dtos.MimboxErrorLog;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;
using Mimbly.Application.Contracts.Dtos.MimboxModel;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // AD
        CreateMap<InviteUserRequestDto, AdUser>()
            .ForMember(
                dest => dest.DisplayName,
                inp => inp.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<AddCompanyRequestDto, AdCompany>();


        // Mimbox
        CreateMap<Mimbox, MimboxDto>();

        CreateMap<CreateMimboxRequestDto, Mimbox>();

        CreateMap<UpdateMimboxRequestDto, Mimbox>();

        // MimboxErrorLog
        CreateMap<UpdateMimboxErrorLogRequestDto, MimboxErrorLog>();

        // MimboxModel
        CreateMap<MimboxModel, MimboxModelDto>();

        CreateMap<CreateMimboxModelRequestDto, MimboxModel>();

        CreateMap<UpdateMimboxModelRequestDto, MimboxModel>();

        // MimboxStatus
        CreateMap<MimboxStatus, MimboxStatusDto>();

        CreateMap<CreateMimboxStatusRequestDto, MimboxStatus>();

        CreateMap<UpdateMimboxStatusRequestDto, MimboxStatus>();

        // MimboxLocation
        CreateMap<MimboxLocation, MimboxLocationDto>();

        CreateMap<CreateMimboxLocationRequestDto, MimboxLocation>();

        CreateMap<UpdateMimboxLocationRequestDto, MimboxLocation>();

        // MimboxContact
        CreateMap<MimboxContact, MimboxContactDto>();

        CreateMap<CreateMimboxContactRequestDto, MimboxContact>();

        CreateMap<UpdateMimboxContactRequestDto, MimboxContact>();

        // MimboxLog

        CreateMap<MimboxLog, MimboxLogDto>();

        CreateMap<CreateMimboxLogRequestDto, MimboxLog>();

        CreateMap<UpdateMimboxLogRequestDto, MimboxLog>();

        // MimboxLogImage

        CreateMap<MimboxLogImage, MimboxLogImageDto>();

        CreateMap<CreateMimboxLogImageRequestDto, MimboxLogImage>();

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

