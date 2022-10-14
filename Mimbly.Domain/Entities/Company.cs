namespace Mimbly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Company")]
public class Company
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Required]
    [Column("Name", TypeName = "Nvarchar(50)")]
    public string Name { get; set; }

    [Column("Parent_Id", TypeName = "uniqueidentifier")]
    public Guid? ParentId { get; set; }

    public List<CompanyContact>? Contacts { get; set; }

    ///Navigation property
    //public virtual Company? ParentCompany { get; set; }

    public Company(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Company()
    {
    }
}
