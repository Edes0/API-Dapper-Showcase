namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class CreateMimboxRequestDtoValidator : AbstractValidator<CreateMimboxRequestDto>
{
    public CreateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model is required");

        RuleFor(x => x.Nickname)
            .Length(1, 50).WithMessage("Nickname length is invalid");
    }
}