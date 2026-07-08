using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.Entities.Usuarios
{
    public class Usuario
    {
        public string? Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = "usuario";
        public bool Estatus { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? UltimoLogin { get; set; }
    }
}
