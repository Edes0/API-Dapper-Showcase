namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class UpdateMimboxRequestDtoValidator : AbstractValidator<UpdateMimboxRequestDto>
{
    public UpdateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("Status id is required");

        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model id is required");

        RuleFor(x => x.Nickname)
            .Length(1, 50).WithMessage("Nickname length is invalid");
    }
}