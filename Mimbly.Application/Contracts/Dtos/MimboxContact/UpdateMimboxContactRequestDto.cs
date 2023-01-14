namespace Mimbly.Application.Contracts.Dtos.MimboxContact;

using System.ComponentModel.DataAnnotations;

public class UpdateMimboxContactRequestDto
{
    [Required]
    [StringLength(55, MinimumLength = 3, ErrorMessage = "Title must be between {2} and {1} characters long.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "Title contains invalid characters.")]
    public string Title { get; set; }

    [Required]
    [StringLength(55, MinimumLength = 2, ErrorMessage = "First name must be between {2} and {1} characters long.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "First name contains invalid characters.")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(55, MinimumLength = 2, ErrorMessage = "Last name must be between {2} and {1} characters long.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "Last name contains invalid characters.")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email provided is not valid.")]
    public string Email { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Phone number provided is not valid.")]
    [Phone(ErrorMessage = "Phone number provided is not valid.")]
    public string PhoneNumber { get; set; }

    [Required]
    public Guid MimboxId { get; set; }
}
