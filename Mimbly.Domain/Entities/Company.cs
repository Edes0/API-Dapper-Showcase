namespace Mimbly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Company")]
public class Company
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Name", TypeName = "Nvarchar(50)")]
    public string Name { get; set; }

    [Column("Parent_Id", TypeName = "uniqueidentifier")]
    public Guid? ParentId { get; set; }

    public ICollection<Company> ChildCompanyList { get; set; } = new List<Company>(); //TODO: Check if needed

    public ICollection<CompanyContact> ContactList { get; set; } = new List<CompanyContact>();

    public ICollection<Mimbox> MimboxList { get; set; } = new List<Mimbox>();

    // Navigation property
    [ForeignKey("ParentId")]
    public virtual Company? ParentCompany { get; set; }


    public Company(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Company()
    {
    }

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
        .HasOne(x => x.ParentCompany)
            .WithMany(x => x.ChildCompanyList)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
