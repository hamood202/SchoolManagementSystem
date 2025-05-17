using DataAccessLayer;
using DataAccessLayer.UserModel;
using Microsoft.AspNetCore.Identity;


namespace WebAPIi.Sevices
{
    public class ContextConfig
    {
        private static readonly string seedAdminEmail = "admin@gmail.com";
        public ContextConfig() { }
        public static async Task SeedDataAsync(SchoolManagementSystemContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await seedUserAsync(userManager, roleManager);
        }

        private static async Task seedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole("Admin");
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            var AdminEmail = seedAdminEmail;
            var Adminuser = await userManager.FindByEmailAsync(AdminEmail);
            if (Adminuser != null)
            {
                var id = Guid.NewGuid().ToString();

                Adminuser = new ApplicationUser
                {
                    Id = id,
                    UserName= AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true,    
                };
                var result = await userManager.CreateAsync(Adminuser,"Admin123");
                await userManager.AddToRoleAsync(Adminuser, "Admin");
            }
        }
    }
}