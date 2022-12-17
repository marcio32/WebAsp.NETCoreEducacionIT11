using Data.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        [ForeignKey("Roles")]
        public int Id_Rol { get; set; }
        public bool Activo { get; set; }
        public int? Codigo { get; set; }
        public string Clave { get; set; }

        public Roles? Roles { get; set; }

        public static implicit operator Usuarios(CrearUsuarioDto crearUsuario)
        {
            var usuario = new Usuarios();
            usuario.Nombre = crearUsuario.Nombre;
            usuario.Apellido = crearUsuario.Apellido;
            usuario.Mail = crearUsuario.Mail;
            usuario.Fecha_Nacimiento = crearUsuario.Fecha_Nacimiento;
            usuario.Id_Rol = 2;
            usuario.Activo = true;
            usuario.Clave = crearUsuario.Clave;
            return usuario;

            
        }
    }
}
