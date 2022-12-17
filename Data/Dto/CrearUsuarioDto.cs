using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Dtos
{
    public class CrearUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        public string Clave { get; set; }

    }

}
