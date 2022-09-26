namespace Boilerplate.Application.Contracts.RequestDtos;

using System.Threading.Tasks;
using Common.Validators;
using CoreServices.Validation;

public class CreateBoilerplateRequestDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateBoilerplateRequestDtoValidator());
    }
}