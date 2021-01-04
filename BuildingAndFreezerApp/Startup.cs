using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using BuildingAndFreezerApp.Models;

[assembly: OwinStartupAttribute(typeof(BuildingAndFreezerApp.Startup))]
namespace BuildingAndFreezerApp
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRole();
            CreateUsers();

        }
        private void CreateUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser();
            user.Email = "hossam50@gmail.com";
            user.UserName = "hossam50@gmail.com";
            var check = userManager.Create(user, "@Hossam77");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");
            }

            //--------------------------------------

            ApplicationUser Admin = new ApplicationUser();
            Admin.Email = "ayman5383@gmail.com";
            Admin.UserName = "ayman5383@gmail.com";
            var checkAdmin = userManager.Create(Admin, "@AymanManger10");
            if (checkAdmin.Succeeded)
            {
                userManager.AddToRole(Admin.Id, "Admins");
            }
            //------------------------------------------------------
            ApplicationUser building = new ApplicationUser();
            building.Email = "MangerBuilding@gamil.com";
            building.UserName = "MangerBuilding@gamil.com";
            var checkbuilding = userManager.Create(building, "@BuildingManger1");
            if (checkbuilding.Succeeded)
            {
                userManager.AddToRole(building.Id, "Building");
            }
            //----------------------------------
            ApplicationUser freezer = new ApplicationUser();
            freezer.Email = "MangerFreezer@gamil.com";
            freezer.UserName = "MangerFreezer@gamil.com";
            var check1 = userManager.Create(freezer, "@FreezerManger1");
            if (check1.Succeeded)
            {
                userManager.AddToRole(freezer.Id, "Freezer");
            }


        }
        private void CreateRole()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManger.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManger.Create(role);
            }
            if (!roleManger.RoleExists("Building"))
            {
                role = new IdentityRole();
                role.Name = "Building";
                roleManger.Create(role);
            }
            if (!roleManger.RoleExists("Freezer"))
            {
                role = new IdentityRole();
                role.Name = "Freezer";
                roleManger.Create(role);
            }
        }
    }
}
