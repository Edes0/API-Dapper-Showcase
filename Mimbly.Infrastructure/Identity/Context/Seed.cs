namespace Mimbly.Infrastructure.Identity.Context;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Enums;

public static class Seed
{
    public static void SeedDataBase(ModelBuilder modelBuilder)
    {
        //////////////////////////////////
        ///         SEED COMPANIES     ///
        //////////////////////////////////

        var companyEntites = new List<Company>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   Name = "Städ Ab",
                },
                new ()
                {
                   Id = Guid.NewGuid(),
                   Name = "Volvo Ab"
                }
        };

        //Setting ParentId for Städ AB.
        companyEntites[0].ParentId = companyEntites[1].Id;

        modelBuilder.Entity<Company>().HasData(companyEntites);

        //////////////////////////////////
        ///     SEED COMPANY CONTACTS  ///
        //////////////////////////////////

        var companyContactEntites = new List<CompanyContact>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   FirstName = "Andreas",
                   LastName = "Sjögren",
                   Email = "sjogrenandreas@live.se",
                   PhoneNumber = "0733143465",
                   CompanyId = companyEntites[0].Id

                },

                new ()
                {
                   Id = Guid.NewGuid(),
                   FirstName = "Hans",
                   LastName = "Sheike",
                   Email = "fdsfsdff@live.se",
                   PhoneNumber = "0739543467",
                   CompanyId = companyEntites[0].Id
                }
        };

        modelBuilder.Entity<CompanyContact>().HasData(companyContactEntites);

        //////////////////////////////////
        ///         SEED LOCATIONS     ///
        //////////////////////////////////

        var locationEntites = new List<Location>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   Country = "Sweden",
                   Region = "Västra götaland",
                   PostalCode = "41729",
                   City = "Göteborg",
                   StreetAddress = "Gamla vägen 18"
                },

                new ()
                {
                   Id = Guid.NewGuid(),
                   Country = "Sweden",
                   Region = "Västra götaland",
                   PostalCode = "41729",
                   City = "Göteborg",
                   StreetAddress = "Nya vägen 2"
                }
        };

        modelBuilder.Entity<Location>().HasData(locationEntites);

        //////////////////////////////////
        ///         SEED MODELS        ///
        //////////////////////////////////

        var modelEntites = new List<MimboxModel>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   Name = ModelType.LaundryRoom
                }
        };

        modelBuilder.Entity<MimboxModel>().HasData(modelEntites);

        //////////////////////////////////
        ///         SEED STATUS        ///
        //////////////////////////////////

        var statusEntites = new List<MimboxStatus>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   Name = StatusType.Broken
                },

                new()
                {
                   Id = Guid.NewGuid(),
                   Name = StatusType.ToBeInstalled
                }
        };

        modelBuilder.Entity<MimboxStatus>().HasData(statusEntites);

        //////////////////////////////////
        ///         SEED LOGS          ///
        //////////////////////////////////

        var mimboxLogEntites = new List<MimboxLog>
        {
                new()
                {
                   Id = Guid.NewGuid(),
                   Log = "This mimxbox is broken"
                },

                new()
                {
                   Id = Guid.NewGuid(),
                   Log = "This mimbox is yet to be installed"
                },
                new()
                {
                   Id = Guid.NewGuid(),
                   Log = "Going to install in next week, lol"
                }
        };

        //////////////////////////////////
        ///         SEED MIMBOXES      ///
        //////////////////////////////////

        var mimboxEntites = new List<Mimbox>
        {
                new()
                {
                    StatusId = statusEntites[0].Id,
                    ModelId = modelEntites[0].Id,
                },

                new ()
                {
                    CompanyId = companyEntites[0].Id,
                    StatusId = statusEntites[0].Id,
                    ModelId = modelEntites[0].Id,
                    LocationId = locationEntites[0].Id,
                }
        };

        // Adding MimboxId to logs
        mimboxLogEntites[0].MimboxId = mimboxEntites[0].Id;
        mimboxLogEntites[1].MimboxId = mimboxEntites[1].Id;
        mimboxLogEntites[1].MimboxId = mimboxEntites[1].Id;

        modelBuilder.Entity<MimboxLog>().HasData(mimboxLogEntites);
        modelBuilder.Entity<Mimbox>().HasData(mimboxEntites);

        modelBuilder.Entity<Mimbox>().OwnsOne(x => x.MimboxLogList).HasData(mimboxLogEntites);
        
    }
}