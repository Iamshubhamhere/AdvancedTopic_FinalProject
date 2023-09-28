using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Areas.Identity.Data;

namespace AdvancedTopic_FinalProject.SeedData
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            ATAuthDemoContext context = new ATAuthDemoContext(serviceProvider.GetRequiredService<DbContextOptions<ATAuthDemoContext>>());
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<DemoUser> UserManager = serviceProvider.GetRequiredService<UserManager<DemoUser>>();

            // Ensure the database is properly set up
           /* context.Database.EnsureDeleted();*/
            context.Database.Migrate();

            // Seeding Roles
            if (!context.Roles.Any())
            {
                List<string> roles = new List<string> { "Administrator", "Project Manager", "Developer" };

                foreach (string s in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(s));
                }

                await context.SaveChangesAsync();
            }

            // Seeding User

            if (!context.Users.Any())
            {
                DemoUser Administrator = new DemoUser()
                {
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser ProjectManagerOne = new DemoUser()
                {
                    UserName = "project@managerone.com",
                    NormalizedUserName = "MANAGERONE",
                    Email = "project@managerone.com",
                    NormalizedEmail = "PROJECT@MANAGERONE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser ProjectManagerTwo = new DemoUser()
                {
                    UserName = "project@managertwo.com",
                    NormalizedUserName = "MANAGERTWO",
                    Email = "project@managertwo.com",
                    NormalizedEmail = "PROJECT@MANAGERTWO.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),

                };

                DemoUser ProjectManagerThree = new DemoUser()
                {
                    UserName = "project@managerthree.com",
                    NormalizedUserName = "MANAGERTHREE",
                    Email = "project@managerthree.com",
                    NormalizedEmail = "PROJECT@MANAGERTHREE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser DevOne = new DemoUser()
                {
                    UserName = "dev@one.com",
                    NormalizedUserName = "DEVELOPERONE",
                    Email = "dev@one.com",
                    NormalizedEmail = "DEV@ONE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser DevTwo = new DemoUser()
                {
                    UserName = "dev@two.com",
                    NormalizedUserName = "DEVELOPERTWO",
                    Email = "dev@two.com",
                    NormalizedEmail = "DEV@TWO.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser DevThree = new DemoUser()
                {
                    UserName = "dev@three.com",
                    NormalizedUserName = "DEVELOPERTHREE",
                    Email = "dev@three.com",
                    NormalizedEmail = "DEV@THREE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                DemoUser DevFour = new DemoUser()
                {
                    UserName = "dev@four.com",
                    NormalizedUserName = "DEVELOPERFOUR",
                    Email = "dev@four.com",
                    NormalizedEmail = "DEV@FOUR.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                HashPasswordGenerator(Administrator, "Admin1!");
                HashPasswordGenerator(ProjectManagerOne, "ProjectManager1!");
                HashPasswordGenerator(ProjectManagerTwo, "ProjectManager2!");
                HashPasswordGenerator(ProjectManagerThree, "ProjectManager3!");
                HashPasswordGenerator(DevOne, "Developer1!");
                HashPasswordGenerator(DevTwo, "Developer2!");
                HashPasswordGenerator(DevThree, "Developer3!");
                HashPasswordGenerator(DevFour, "Developer4!");

                context.Users.Add(Administrator);
                context.Users.Add(ProjectManagerOne);
                context.Users.Add(ProjectManagerTwo);
                context.Users.Add(ProjectManagerThree);
                context.Users.Add(DevOne);
                context.Users.Add(DevTwo);
                context.Users.Add(DevThree);
                context.Users.Add(DevFour);

                await context.SaveChangesAsync();

                DemoUser adminUser = await UserManager.FindByNameAsync("Admin");
                DemoUser pmOneUser = await UserManager.FindByNameAsync("ManagerOne");
                DemoUser pmTwoUser = await UserManager.FindByNameAsync("ManagerTwo");
                DemoUser pmThreeUser = await UserManager.FindByNameAsync("ManagerThree");
                DemoUser devOne = await UserManager.FindByNameAsync("DeveloperOne");
                DemoUser devTwo = await UserManager.FindByNameAsync("DeveloperTwo");
                DemoUser devThree = await UserManager.FindByNameAsync("DeveloperThree");
                DemoUser devFour = await UserManager.FindByNameAsync("DeveloperFour");


                await UserManager.AddToRoleAsync(adminUser, "Administrator");
                await UserManager.AddToRoleAsync(pmOneUser, "Project Manager");
                await UserManager.AddToRoleAsync(pmTwoUser, "Project Manager");
                await UserManager.AddToRoleAsync(pmThreeUser, "Project Manager");
                await UserManager.AddToRoleAsync(devOne, "Developer");
                await UserManager.AddToRoleAsync(devTwo, "Developer");
                await UserManager.AddToRoleAsync(devThree, "Developer");
                await UserManager.AddToRoleAsync(devFour, "Developer");

                await context.SaveChangesAsync();
            }

            context.SaveChanges();

        }

        // Generating Hash Password
        public static string HashPasswordGenerator(DemoUser user, string password)
        {
            PasswordHasher<DemoUser> passwordHasher = new PasswordHasher<DemoUser>();
            string hashedPassword = passwordHasher.HashPassword(user, password);

            return user.PasswordHash = hashedPassword;
        }
    }
}
