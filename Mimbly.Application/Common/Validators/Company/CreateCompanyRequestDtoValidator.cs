﻿namespace Mimbly.Application.Common.Validators.Company;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Company;

public class CreateCompanyRequestDtoValidator : AbstractValidator<CreateCompanyRequestDto>
{
    public CreateCompanyRequestDtoValidator()
    {
        RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required");
    }
}
