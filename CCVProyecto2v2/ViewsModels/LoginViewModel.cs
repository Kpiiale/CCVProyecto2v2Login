using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CCVProyecto2v2.Models;

namespace CCVProyecto2v2.ViewsModels
{
    public class LoginViewModel
    {
        private readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            return await _authService.LoginAsync(username, password);
        }
    }
}
