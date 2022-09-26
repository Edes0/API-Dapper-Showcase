namespace Boilerplate.Application.Common.Mappings;

using AutoMapper;
using Boilerplate.Application.Contracts.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile() => CreateMap<Domain.Models.Boilerplate, BoilerplateDto>();
}