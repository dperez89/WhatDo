using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WhatDo.Models;

[assembly: OwinStartupAttribute(typeof(WhatDo.Startup))]
namespace WhatDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));




            // creating Creating Promoter role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
            }
            // Creating Attendee role    
            if (!roleManager.RoleExists("Enjoyer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Enjoyer"
                };
                roleManager.Create(role);

            }
        }
    }
}
