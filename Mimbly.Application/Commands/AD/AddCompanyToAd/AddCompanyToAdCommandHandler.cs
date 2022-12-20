namespace Mimbly.Application.Commands.AD.AddCompanyToAd;

using AutoMapper;
using Business.Interfaces.AD;
using Domain.Entities.AD;
using MediatR;

public class AddCompanyToAdCommandHandler : IRequestHandler<AddCompanyToAdCommand, string?>
{
    private readonly IAccountService _ac;
    private readonly IMapper _mapper;

    public AddCompanyToAdCommandHandler(IAccountService ac, IMapper mapper)
    {
        _ac = ac;
        _mapper = mapper;
    }

    public async Task<string?> Handle(AddCompanyToAdCommand request, CancellationToken cancellationToken)
    {
        await request.AddCompanyToAdRequest.Validate();

        var company = _mapper.Map<AdCompany>(request.AddCompanyToAdRequest);

        return await _ac.CreateCompany(company);

    }
}