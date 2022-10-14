namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Location")]
public class Location
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Required]
    [Column("Country", TypeName = "Nvarchar(100)")]
    public string Country { get; set; }

    [Required]
    [Column("Region", TypeName = "Nvarchar(100)")]
    public string? Region { get; set; }

    [Required]
    [Column("Postal_code", TypeName = "Varchar(5)")]
    public string? PostalCode { get; set; }

    [Required]
    [Column("City", TypeName = "Nvarchar(100)")]
    public string City { get; set; }

    [Required]
    [Column("Street_Address", TypeName = "Nvarchar(100)")]
    public string StreetAddress { get; set; }


    public Location(string country, string city, string streetAddress)
    {
        Id = Guid.NewGuid();
        Country = country;
        City = city;
        StreetAddress = streetAddress;
    }

    public Location()
    {
    }
}
