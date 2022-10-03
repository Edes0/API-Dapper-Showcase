namespace Mimbly.Application.Common.Mappings;

using AutoMapper;
using Mimbly.Application.Contracts.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile() => CreateMap<Domain.Models.Mimbly, MimblyDto>();
}