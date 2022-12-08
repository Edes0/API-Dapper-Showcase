namespace Mimbly.Domain.Entities.POCOs;

using Mimbly.Domain.Entities.AzureEvents;

public class MimboxPoco
{

    public MimboxContact MimboxContact => new MimboxContact
    {
        Id = Id,
        Title = Title,
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        PhoneNumber = PhoneNumber,
        MimboxId = MimboxId
    };

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid MimboxId { get; set; }

    public MimboxLocation MimboxLocation { get; set; }
    public MimboxLog MimboxLog { get; set; }
    public MimboxModel MimboxModel { get; set; }
    public MimboxStatus MimboxStatus { get; set; }
    public MimboxErrorLog MimboxErrorLog { get; set; }
}
