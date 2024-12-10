using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCVProyecto2v2.Models;
using Microsoft.AspNetCore.Identity;

namespace CCVProyecto2v2.DataAccess
{
    public class DataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (await _userManager.FindByNameAsync("admin") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Nombre = "Administrador del Sistema",
                    Rol = RolEnum.Administrador
                };

                await _userManager.CreateAsync(admin, "admin");
                await _userManager.AddToRoleAsync(admin, "Administrador");
            }
        }
    }
}

