namespace Mimbly.Application.Queries.MimboxContact.GetAll;

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxContact;
using Mimbly.Application.Queries.MimboxContact.GetAll;

public class GetAllMimboxContactsHandler : IRequestHandler<GetAllMimboxContactsQuery, AllMimboxContactsVm>
{
    private readonly IMimboxContactRepository _MimboxContactRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxContactsHandler(
        IMimboxContactRepository MimboxContactRepository,
        IMapper mapper)
    {
        _MimboxContactRepository = MimboxContactRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxContactsVm> Handle(GetAllMimboxContactsQuery request, CancellationToken cancellationToken)
    {
        var MimboxContacts = await _MimboxContactRepository.GetAllMimboxContacts();

        var MimboxContactsDtos = _mapper.Map<IEnumerable<MimboxContactDto>>(MimboxContacts);

        return new AllMimboxContactsVm { MimboxContacts = MimboxContactsDtos };
    }
}