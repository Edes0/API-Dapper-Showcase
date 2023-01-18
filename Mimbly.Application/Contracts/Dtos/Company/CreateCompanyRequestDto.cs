namespace Mimbly.Application.Contracts.Dtos.Company;

using System;
using System.ComponentModel.DataAnnotations;

public class CreateCompanyRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public Guid Id { get; set; }

    [Required]
    [StringLength(55, MinimumLength = 3, ErrorMessage = "Name must be between {2} and {1} characters long.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "Name contains invalid characters.")]
    public string Name { get; set; }

    public Guid? ParentId { get; set; }
}
