using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 

public enum RolEnum
{
    Administrador,
    Estudiante,
    Profesor
}

namespace CCVProyecto2v2.Models
{
    internal class ApplicationUser: IdentityUser
    {
        public string Nombre { get; set; }
        public RolEnum Rol { get; set; }
    }
}
