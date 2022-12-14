namespace Mimbly.Api.AAD.DTOs;

public class CompanyModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid ParentId { get; set; }
}
