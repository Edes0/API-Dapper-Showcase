namespace Mimbly.Application.Queries.Company.GetWithAllDataById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mimbly.Application.Common.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetFilterWithAllDataByIdCompanyHandler : IRequestHandler<GetFilterWithChildrenByIdCompanyQuery, CompanyWithChildrenFilteredById>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetFilterWithAllDataByIdCompanyHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyWithChildrenFilteredById> Handle(GetFilterWithChildrenByIdCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyWithChildrenById(request.Id);

        if (company.IsNullOrEmpty())
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(company.First());

        return new CompanyWithChildrenFilteredById
        {
            Companies = companyDtos
        };
    }
}