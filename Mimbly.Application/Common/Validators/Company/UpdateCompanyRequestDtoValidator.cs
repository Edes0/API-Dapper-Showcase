﻿namespace Mimbly.Application.Common.Validators.Company;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Company;

public class UpdateCompanyRequestDtoValidator : AbstractValidator<UpdateCompanyRequestDto>
{
    public UpdateCompanyRequestDtoValidator()
    {
        RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required");
    }
}