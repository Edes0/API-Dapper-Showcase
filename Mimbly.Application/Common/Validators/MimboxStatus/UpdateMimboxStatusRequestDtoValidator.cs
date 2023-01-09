namespace Mimbly.Application.Common.Validators.MimboxStatus;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class UpdateMimboxStatusRequestDtoValidator : AbstractValidator<UpdateMimboxStatusRequestDto>
{
    public UpdateMimboxStatusRequestDtoValidator()
    {
        RuleFor(x => x.Name)
             .NotNull().WithMessage("Name is required");
    }
}
