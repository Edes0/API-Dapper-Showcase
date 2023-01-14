namespace Mimbly.Application.Contracts.Dtos.MimboxContact;

using System.ComponentModel.DataAnnotations;

public class UpdateMimboxContactRequestDto
{
    [Required]
    [MinLength(3,
        ErrorMessage = "Title is too short, it should at least be 3 characters long.")]
    [MaxLength(55,
        ErrorMessage = "Title is too long, it is limited to 55 characters.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "Title contains invalid characters.")]
    public string Title { get; set; }

    [Required]
    [MinLength(2,
        ErrorMessage = "First name is too short, it should at least be 2 characters long.")]
    [MaxLength(55,
        ErrorMessage = "First name is too long, it is limited to 55 characters.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "First name contains invalid characters.")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2,
        ErrorMessage = "Last name is too short, it should at least be 2 characters long.")]
    [MaxLength(55,
        ErrorMessage = "Last name is too long, it is limited to 55 characters.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "Last name contains invalid characters.")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email provided is not valid.")]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Phone number provided is not valid.")]
    [MaxLength(11, ErrorMessage = "Phone number provided is not valid.")]
    [Phone(ErrorMessage = "Phone number provided is not valid.")]
    public string PhoneNumber { get; set; }

    [Required]
    public Guid MimboxId { get; set; }
}
