namespace Mimbly.Application.Queries.MimboxContact.GetById;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxContact;
using Mimbly.Application.Queries.MimboxContact.GetById;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxContactHandler : IRequestHandler<GetByIdMimboxContactQuery, MimboxContactByIdVm>
{
    private readonly IMimboxContactRepository _MimboxContactRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxContactHandler(
        IMimboxContactRepository MimboxContactRepository,
        IMapper mapper)
    {
        _MimboxContactRepository = MimboxContactRepository;
        _mapper = mapper;
    }

    public async Task<MimboxContactByIdVm> Handle(GetByIdMimboxContactQuery request, CancellationToken cancellationToken)
    {
        var MimboxContact = await _MimboxContactRepository.GetMimboxContactById(request.Id);

        if (MimboxContact == null)
            throw new NotFoundException($"Can't find Mimbox contact with id: {request.Id}");

        var MimboxContactDto = _mapper.Map<MimboxContactDto>(MimboxContact);

        return new MimboxContactByIdVm { MimboxContact = MimboxContactDto };
    }
}
