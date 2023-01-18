namespace Mimbly.Domain.DocumentModels;

using System;

public class ReportModel
{
    public DateTime Created => DateTime.Now;
    public string Month => GetMonth();
    public Company Company { get; set; }
    public Stats Stats { get; set; }
    public ICollection<Address> BestMimboxes { get; set; }

    private static string GetMonth()
    {
        string[] monthNames = { "January", "February", "March", "April", "May" ,"June", "July", "August", "September", "October", "November", "December" };
        var monthNumber = DateTime.Now.Month - 1;

        return monthNames[monthNumber];
    }
}

public class Stats
{
    public int PlasticSaved { get; set; }
    public string MoneySaved { get; set; }
    public int WaterSaved { get; set; }
}

public class Company
{
    public string Name { get; set; }
}

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string PostCode { get; set; }
}