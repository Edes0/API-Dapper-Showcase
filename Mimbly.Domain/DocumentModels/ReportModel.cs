namespace Mimbly.Domain.DocumentModels;

using System;

public class ReportModel
{
    public DateTime Created => DateTime.Now;
    public string Month => getMonth();
    public Company Company { get; set; }
    public Stats Stats { get; set; }

    private string getMonth()
    {
        string[] monthNames = { "January", "February", "March", "April", "May" ,"June", "July", "August", "September", "October", "November", "December" };
        int monthNumber = DateTime.Now.Month - 1;

        return monthNames[monthNumber];
    }
}

public class Stats
{
    public int PlasticSaved { get; set; }
    public int PlasticSavedLastMonth { get; set; }
    public int CarbonSaved { get; set; }
    public int CarbonSavedLastMonth { get; set; }
    public int WaterSaved { get; set; }
    public int WaterSavedLastMonth { get; set; }
}

public class Company
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public Guid Id => Guid.NewGuid();

}

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string PostCode { get; set; }
}