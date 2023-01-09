namespace Mimbly.Domain.Entities.AD;

public class AdCompany
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid ParentId { get; set; }
}