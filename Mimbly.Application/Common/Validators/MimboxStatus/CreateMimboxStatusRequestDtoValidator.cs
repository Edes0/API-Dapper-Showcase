namespace Mimbly.Application.Common.Validators.MimboxStatus;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class CreateMimboxStatusRequestDtoValidator : AbstractValidator<CreateMimboxStatusRequestDto>
{
    public CreateMimboxStatusRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required");
    }
}
