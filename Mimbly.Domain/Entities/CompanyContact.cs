namespace Mimbly.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Company_Contact")]
public class CompanyContact
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("First_name", TypeName = "Nvarchar(50)")]
    public string FirstName { get; set; }

    [Column("Last_name", TypeName = "Nvarchar(50)")]
    public string LastName { get; set; }

    [Column("Email", TypeName = "Varchar(100)")]
    public string Email { get; set; }

    [Column("Phone_number", TypeName = "Varchar(15)")]
    public string PhoneNumber { get; set; }

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid CompanyId { get; set; }

    // Navigation property
    [ForeignKey("CompanyId")]
    public virtual Company Company { get; set; }


    public CompanyContact(string firstName, string lastName, string email, string phoneNumber)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public CompanyContact()
    {
    }
}



