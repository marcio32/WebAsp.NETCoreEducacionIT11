﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class UsuariosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        public int Id_Rol { get; set; }
        public bool Activo { get; set; }
        public int? Codigo { get; set; }
        public string Clave { get; set; }
    }
}
