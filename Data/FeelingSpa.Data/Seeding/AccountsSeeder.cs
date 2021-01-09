namespace FeelingSpa.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Helpers;

    using FeelingSpa.Common;
    using FeelingSpa.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class AccountsSeeder : ISeeder
    {
        private const string UsersPassword = "123456";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (dbContext.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser
            {
                UserName = "",
                Email = "",
                EmailConfirmed = true,
            };
            var manager = new ApplicationUser
            {
                UserName = "",
                Email = "",
                EmailConfirmed = true,
            };

            await SeedUser(userManager, admin, UsersPassword, GlobalConstants.AdministratorRoleName);
            await SeedUser(userManager, manager, UsersPassword, "Manager");
        }

        private static async Task SeedUser(UserManager<ApplicationUser> userManager, ApplicationUser user,
            string password, string roleName)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
