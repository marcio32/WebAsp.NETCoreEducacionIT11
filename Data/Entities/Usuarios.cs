using Data.Dto;
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

        public static implicit operator Usuarios(CrearUsuarioDto crearUsuarioDto)
        {
            var usuario = new Usuarios();
            usuario.Nombre = crearUsuarioDto.Nombre;
            usuario.Apellido = crearUsuarioDto.Apellido;
            usuario.Mail = crearUsuarioDto.Mail;
            usuario.Fecha_Nacimiento = crearUsuarioDto.Fecha_Nacimiento;
            usuario.Id_Rol = 2;
            usuario.Activo = true;
            usuario.Clave = crearUsuarioDto.Clave;
            return usuario;

            
        }

        public static implicit operator Usuarios(UsuariosDto usuarioDto)
        {
            var usuario = new Usuarios();
            usuario.Id = usuarioDto.Id;
            usuario.Nombre = usuarioDto.Nombre;
            usuario.Apellido = usuarioDto.Apellido;
            usuario.Mail = usuarioDto.Mail;
            usuario.Fecha_Nacimiento = usuarioDto.Fecha_Nacimiento;
            usuario.Id_Rol = usuarioDto.Id_Rol;
            usuario.Activo = usuarioDto.Activo;
            usuario.Clave = usuarioDto.Clave;
            return usuario;

        }

        public static implicit operator Usuarios(LoginDto loginDto)
        {
            var usuario = new Usuarios();
            loginDto.Mail = usuario.Mail;
            loginDto.Codigo = usuario.Codigo;
            loginDto.Clave = usuario.Clave;
            return usuario;
        }
    }
}
