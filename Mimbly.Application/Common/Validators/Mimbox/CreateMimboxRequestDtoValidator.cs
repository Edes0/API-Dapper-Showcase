namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class CreateMimboxRequestDtoValidator : AbstractValidator<CreateMimboxRequestDto>
{
    public CreateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.StatusId)
            .NotNull().WithMessage("Status is required");

        RuleFor(x => x.ModelId)
            .NotNull().WithMessage("Model is required");
    }
}