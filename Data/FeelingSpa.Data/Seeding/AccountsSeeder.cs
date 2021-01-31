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
        private const string UsersPassword = GlobalConstants.AccountsSeeding.Password;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (dbContext.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser
            {
                UserName = GlobalConstants.AccountsSeeding.AdminEmail,
                Email = GlobalConstants.AccountsSeeding.AdminEmail,
                EmailConfirmed = true,
            };
            var manager = new ApplicationUser
            {
                UserName = GlobalConstants.AccountsSeeding.SalonManagerEmail,
                Email = GlobalConstants.AccountsSeeding.SalonManagerEmail,
                EmailConfirmed = true,
            };

            await SeedUser(userManager, admin, UsersPassword, GlobalConstants.AdministratorRoleName);
            await SeedUser(userManager, manager, UsersPassword, GlobalConstants.SalonManagerRoleName);
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
