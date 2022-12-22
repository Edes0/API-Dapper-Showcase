namespace Mimbly.Application.Common.Validators.MimboxModel;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxModel;

public class CreateMimboxModelRequestDtoValidator : AbstractValidator<CreateMimboxModelRequestDto>
{
    public CreateMimboxModelRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required");
    }
}
