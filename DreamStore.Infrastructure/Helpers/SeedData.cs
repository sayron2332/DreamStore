﻿using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Product;
using DreamStore.Core.Interfaces;
using DreamStore.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Infrastructure
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var roleService = serviceProvider.GetRequiredService<IRoleService>();

            // Створення ролей, якщо вони не існують
            async Task SeedUsersAndRoles()
            {
                var roleExist = await roleService.GetRoleByNameAsync("admin");
                if (roleExist == null)
                {
                    AppRole[] identityRoles =
                    {
                     new AppRole {Name = "user"},
                     new AppRole {Name = "admin"}
                };
                    foreach (var role in identityRoles)
                    {
                        await context.AppRoles.AddAsync(role);
                    }
                    await context.SaveChangesAsync();
                }


                var AdminRole = await roleService.GetRoleByNameAsync("admin");
                if (!context.AppUsers.Any())
                {
                    var hasher = new PasswordHasher<AppUser>();
                    var user = new AppUser
                    {
                        Name = "Nazar",
                        Surname = "Kurylovych",
                        Email = "admin@example.com",
                        PhoneNumber = "+380959348105",
                        PasswordHash = hasher.HashPassword(null!, "Admin123!"),
                        RoleId = AdminRole!.Id
                    };

                    // Створення користувача
                    var result = await context.AppUsers.AddAsync(user);
                    await context.SaveChangesAsync();

                }
            };

            //async Task SeedProductAndAttributes()
            //{
            //    new AppAttribute { Name = "Gpu", Value = "Discrete",  };

            //};
            //await SeedProductAndAttributes();
            await SeedUsersAndRoles();
          
        }
    }
}