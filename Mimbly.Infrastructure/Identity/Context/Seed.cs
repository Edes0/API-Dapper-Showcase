namespace Mimbly.Infrastructure.Identity.Context;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Entities;

public static class Seed
{
    public static void SeedDataBase(ModelBuilder modelBuilder)
    {
        ////////////////////////////////////
        /////         SEED COMPANIES     ///
        ////////////////////////////////////

        //var companyEntites = new List<Company>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Städ Ab",
        //        },
        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Volvo Ab"
        //        },
        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Saab Ab"
        //        },
        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Snickar Ab"
        //        },
        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Pampers Ab"
        //        }
        //};

        ////Setting ParentId for Städ AB.
        //for (int i = 0, u = 1; i < companyEntites.Count - 1; i++, u++)
        //{
        //    companyEntites[i].ParentId = companyEntites[u].Id;
        //}

        //modelBuilder.Entity<Company>().HasData(companyEntites);

        ////////////////////////////////////
        /////     SEED COMPANY CONTACTS  ///
        ////////////////////////////////////

        //var companyContactEntites = new List<CompanyContact>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           FirstName = "Andreas",
        //           LastName = "Sjögren",
        //           Email = "sjogrenandreas@live.se",
        //           PhoneNumber = "0733143465",
        //           CompanyId = companyEntites[0].Id,
        //           Title = "Company VD"
        //        },

        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           FirstName = "Hans",
        //           LastName = "Andersson",
        //           Email = "HansAndersson@live.se",
        //           PhoneNumber = "0737543467",
        //           CompanyId = companyEntites[0].Id,
        //           Title = "Company something"
        //        }
        //};

        //modelBuilder.Entity<CompanyContact>().HasData(companyContactEntites);

        ////////////////////////////////////
        /////         SEED LOCATIONS     ///
        ////////////////////////////////////

        //var locationEntites = new List<MimboxLocation>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Country = "Sweden",
        //           Region = "Västra götaland",
        //           PostalCode = "41729",
        //           City = "Göteborg",
        //           StreetAddress = "Gamla vägen 18"
        //        },

        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           Country = "Sweden",
        //           Region = "Västra götaland",
        //           PostalCode = "41729",
        //           City = "Göteborg",
        //           StreetAddress = "Nya vägen 2"
        //        }
        //};

        //modelBuilder.Entity<MimboxLocation>().HasData(locationEntites);

        ////////////////////////////////////
        /////         SEED MODELS        ///
        ////////////////////////////////////

        //var modelEntites = new List<MimboxModel>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Laundry"
        //        }
        //};

        //modelBuilder.Entity<MimboxModel>().HasData(modelEntites);

        ////////////////////////////////////
        /////         SEED STATUS        ///
        ////////////////////////////////////

        //var statusEntites = new List<MimboxStatus>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "Broken"
        //        },

        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Name = "ToBeInstalled"
        //        }
        //};

        //modelBuilder.Entity<MimboxStatus>().HasData(statusEntites);

        ////////////////////////////////////
        /////         SEED MIMBOXES      ///
        ////////////////////////////////////

        //var mimboxEntites = new List<Mimbox>
        //{
        //        new()
        //        {
        //            StatusId = statusEntites[0].Id,
        //            ModelId = modelEntites[0].Id
        //        },

        //        new ()
        //        {
        //            CompanyId = companyEntites[0].Id,
        //            StatusId = statusEntites[0].Id,
        //            ModelId = modelEntites[0].Id,
        //            LocationId = locationEntites[0].Id
        //        },

        //        new ()
        //        {
        //            CompanyId = companyEntites[0].Id,
        //            StatusId = statusEntites[0].Id,
        //            ModelId = modelEntites[0].Id,
        //            LocationId = locationEntites[0].Id
        //        },

        //        new ()
        //        {
        //            CompanyId = companyEntites[0].Id,
        //            StatusId = statusEntites[1].Id,
        //            ModelId = modelEntites[0].Id,
        //            LocationId = locationEntites[1].Id
        //        }
        //};

        //modelBuilder.Entity<Mimbox>().HasData(mimboxEntites);

        ////////////////////////////////////
        /////     SEED MIMBOX CONTACTS  ///
        ////////////////////////////////////

        //var mimboxContactEntites = new List<MimboxContact>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           FirstName = "Edwin",
        //           LastName = "Andersson",
        //           Email = "sjfdsfeas@live.se",
        //           PhoneNumber = "0733143465",
        //           MimboxId = mimboxEntites[1].Id,
        //           Title = "Company VD"
        //        },

        //        new ()
        //        {
        //           Id = Guid.NewGuid(),
        //           FirstName = "Kristoffer",
        //           LastName = "Karlsson",
        //           Email = "fdsfsdff@live.se",
        //           PhoneNumber = "0739543467",
        //           MimboxId = mimboxEntites[1].Id,
        //           Title = "Company VD"
        //        }
        //};

        //modelBuilder.Entity<MimboxContact>().HasData(mimboxContactEntites);

        ////////////////////////////////////
        /////         SEED LOGS          ///
        ////////////////////////////////////

        //var mimboxLogEntites = new List<MimboxLog>
        //{
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Log = "This mimxbox is broken",
        //           MimboxId = mimboxEntites[0].Id
        //        },

        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Log = "This mimbox is yet to be installed",
        //           MimboxId = mimboxEntites[1].Id
        //        },
        //        new()
        //        {
        //           Id = Guid.NewGuid(),
        //           Log = "Going to install in next week",
        //           MimboxId = mimboxEntites[1].Id
        //        }
        //};

        //modelBuilder.Entity<MimboxLog>().HasData(mimboxLogEntites);
    }
}
