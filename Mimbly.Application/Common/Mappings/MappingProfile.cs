namespace Mimbly.Application.Common.Mappings;

using AutoMapper;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Mimbox mapping
        CreateMap<Mimbox, MimboxDto>();

        CreateMap<CreateMimboxRequestDto, Mimbox>();

        //Company mapping
        CreateMap<Company, CompanyDto>();
    }
}
