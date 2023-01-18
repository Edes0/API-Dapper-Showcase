namespace Mimbly.Application.Contracts.Dtos.MimboxErrorLog;

using System.ComponentModel.DataAnnotations;

public class UpdateMimboxErrorLogRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public bool Discarded { get; set; }
}