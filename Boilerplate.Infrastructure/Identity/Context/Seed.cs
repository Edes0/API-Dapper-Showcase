namespace Boilerplate.Infrastructure.Identity;

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

        var boilerplateModels = new List<Domain.Models.Boilerplate>
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

        modelBuilder.Entity<Domain.Models.Boilerplate>().HasData(boilerplateModels);
    }
}