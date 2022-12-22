namespace Mimbly.Application.Common.Validators.MimboxModel;

using Mimbly.Application.Contracts.Dtos.MimboxModel;
using FluentValidation;

public class UpdateMimboxModelRequestDtoValidator : AbstractValidator<UpdateMimboxModelRequestDto>
{
    public UpdateMimboxModelRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required");
    }
}
