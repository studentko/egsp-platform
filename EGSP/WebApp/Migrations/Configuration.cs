namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { /*Id = "admin",*/ UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                var userId = userManager.Create(user);
                Console.WriteLine("User id: " + user.Id);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "controller@yahoo.com"))
            {
                var user = new ApplicationUser() { /*Id = "admin",*/ UserName = "controller@yahoo.com", Email = "controller@yahoo.com", PasswordHash = ApplicationUser.HashPassword("123123") };
                var userId = userManager.Create(user);
                Console.WriteLine("User id: " + user.Id);
                userManager.AddToRole(user.Id, "Controller");
            }

            /*if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }*/

            if (!context.CustomerTypes.Any(ct => ct.DisplayName == "Djacka"))
            {
                CustomerType ctype = new CustomerType()
                {
                    DisplayName = "Djacka",
                    NeedApproval = true,
                    NeedPhotoId = true,
                    InstructionsToUser = "Za osnovnoskolce, srednjoskolce i studente. Dostaviti sliku djacke knjizice ili indexa u dokumentima."
                };
                context.CustomerTypes.Add(ctype);
            }
            if (!context.CustomerTypes.Any(ct => ct.DisplayName == "Regularna"))
            {
                CustomerType ctype = new CustomerType()
                {
                    DisplayName = "Regularna",
                    NeedApproval = true,
                    NeedPhotoId = false,
                    InstructionsToUser = "Za zaposlene i nezaposlene."    
                };
                context.CustomerTypes.Add(ctype);
            }
            if (!context.CustomerTypes.Any(ct => ct.DisplayName == "Penzionerska"))
            {
                CustomerType ctype = new CustomerType()
                {
                    DisplayName = "Penzionerska",
                    NeedApproval = true,
                    NeedPhotoId = true,
                    InstructionsToUser = "Za penzionere. Dostaviti sliku penzionog ceka u dokumentima."
                };
                context.CustomerTypes.Add(ctype);
            }

            context.SaveChanges();
        }
    }
}
