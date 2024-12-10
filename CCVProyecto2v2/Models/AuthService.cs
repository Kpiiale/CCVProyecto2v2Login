using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CCVProyecto2v2.Models
{
    public class AuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {

                var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> RegisterAsync(string username, string password, RolEnum1 rol, string nombre, int edad)
        {
            try
            {

                var user = new ApplicationUser
                {
                    UserName = username,
                    Nombre = nombre,
                    Rol = rol
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, rol.ToString());
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
                return false;
            }
        }
    }
}