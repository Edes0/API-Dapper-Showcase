namespace Mimbly.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Contact")]
public class MimboxContact
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Title", TypeName = "Nvarchar(50)")]
    public string Title { get; set; }

    [Column("First_name", TypeName = "Nvarchar(50)")]
    public string FirstName { get; set; }

    [Column("Last_name", TypeName = "Nvarchar(50)")]
    public string LastName { get; set; }

    [Column("Email", TypeName = "Varchar(100)")]
    public string Email { get; set; }

    [Column("Phone_number", TypeName = "Varchar(15)")]
    public string PhoneNumber { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    public MimboxContact(string firstName, string lastName, string email, string phoneNumber, string title)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Title = title;
    }

    public MimboxContact()
    {
    }
}



