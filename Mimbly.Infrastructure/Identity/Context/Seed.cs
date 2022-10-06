namespace Mimbly.Infrastructure.Identity.Context;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public static class Seed
{
    public static void SeedDataBase(ModelBuilder modelBuilder)
    {
        // Initial user with password password123

        //////////////////////////////////
        ///         SEED USER          ///
        //////////////////////////////////

        var MimblyModels = new List<Domain.Enitites.Mimbox>
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

        modelBuilder.Entity<Domain.Enitites.Mimbox>().HasData(MimblyModels);
    }
}