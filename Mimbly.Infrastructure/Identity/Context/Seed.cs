namespace Mimbly.Infrastructure.Identity.Context;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Enitites;

public static class Seed
{
    public static void SeedDataBase(ModelBuilder modelBuilder)
    {
        // Initial user with password password123

        //////////////////////////////////
        ///         SEED USER          ///
        //////////////////////////////////

        var mimboxEntites = new List<Mimbox>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Daniel",
                    LastName = "Persson",
                    Age = 31
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Rundberg",
                    LastName = "Rundbergsson",
                    Age = 33
                }
            };

        modelBuilder.Entity<Mimbox>().HasData(mimboxEntites);
    }
}