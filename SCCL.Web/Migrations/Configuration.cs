namespace SCCL.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SCCL.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SCCL.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SCCL.Web.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                };

            IdentityResult result = userManager.Create(user, "Password@1");

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrator" });
            context.SaveChanges();
            userManager.AddToRole(user.Id, "Administrator");
            context.SaveChanges();

            //Roles Based on Portal
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Employee" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "BlogUser" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "ProjectUser" });

            context.SaveChanges();
        }
    }
}
