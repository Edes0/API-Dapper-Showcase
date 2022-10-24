namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Location")]
public class MimboxLocation
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Country", TypeName = "Nvarchar(100)")]
    public string Country { get; set; }

    [Column("Region", TypeName = "Nvarchar(100)")]
    public string? Region { get; set; }

    [Column("Postal_code", TypeName = "Varchar(5)")]
    public string? PostalCode { get; set; }

    [Column("City", TypeName = "Nvarchar(100)")]
    public string City { get; set; }

    [Column("Street_Address", TypeName = "Nvarchar(100)")]
    public string StreetAddress { get; set; }

    public MimboxLocation(string country, string city, string streetAddress)
    {
        Id = Guid.NewGuid();
        Country = country;
        City = city;
        StreetAddress = streetAddress;
    }

    public MimboxLocation()
    {
    }
}
