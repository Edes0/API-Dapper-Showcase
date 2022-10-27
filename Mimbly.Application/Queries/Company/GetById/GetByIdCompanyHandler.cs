namespace Mimbly.Application.Queries.Company.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mimbly.Application.Common.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetByIdCompanyHandler : IRequestHandler<GetByIdCompanyQuery, CompanyByIdVm>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetByIdCompanyHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyByIdVm> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyById(request.Id);

        if (company == null)
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyDto = _mapper.Map<CompanyDto>(company);

        return new CompanyByIdVm { Company = companyDto };
    }
}