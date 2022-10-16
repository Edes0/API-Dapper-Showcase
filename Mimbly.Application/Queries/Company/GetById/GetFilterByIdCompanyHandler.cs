namespace Mimbly.Application.Queries.Company.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Mimbly.Application.Common.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetFilterByIdCompanyHandler : IRequestHandler<GetFilterByIdCompanyQuery, CompanyFilteredById>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetFilterByIdCompanyHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyFilteredById> Handle(GetFilterByIdCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyById(request.Id);

        if (company.IsNullOrEmpty())
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyDto = _mapper.Map<CompanyDto>(company.First());

        return new CompanyFilteredById
        {
            Company = companyDto
        };
    }
}