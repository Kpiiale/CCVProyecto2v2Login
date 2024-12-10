using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public enum RolEnum1
{
    Administrador,
    Estudiante,
    Profesor
}

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Nombre { get; set; }

    [Required]
    public RolEnum1 Rol { get; set; }
}

