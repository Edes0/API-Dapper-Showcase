namespace Mimbly.Application.Commands.Mimbox.DeleteMimbox;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;
using Mimbly.Domain.Enitites;

public class CreateMimblyCommandHandler : IRequestHandler<DeleteMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;

    public CreateMimblyCommandHandler(IMimboxRepository mimboxRepository)
    {
        _mimboxRepository = mimboxRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxCommand request, CancellationToken cancellationToken)
    {

        //var order = await _orderRepository.GetOrderById(request.Id);

        //if (order == null)
        //{
        //    throw new NotFoundException(TranslationHandler.Translate("CANT_FIND_ORDER_WITH_ID") + $": {request.Id}");
        //}

        // TODO: Dynamic? DynamicRepository? Find<T> GetItemAndCheckIfExists etc


        await _mimboxRepository.DeleteMimbox(request.Id);

        return Unit.Value;
    }
}