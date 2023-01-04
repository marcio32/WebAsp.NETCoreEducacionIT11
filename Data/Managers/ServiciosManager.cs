using Data.Base;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class ServiciosManager : BaseManager<Servicios>
    {
        public override Task<Servicios> Buscar()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Servicios>> BuscarLista()
        {

            return contextoSingleton.Servicios.FromSqlRaw("ObtenerServicios").ToList();
            //var respuesta = contextoSingleton.Servicios.FromSqlRaw("SELECT * FROM SERVICIOS WHERE ACTIVO = {0}", Activo);

            /*public async Task<IEnumerable<Procesados>> Obtener(DateTime fecha)
            {
                DateTime f = new DateTime(fecha.Year, fecha.Month, fecha.Day);
                using var connection = new SqlConnection(connectionString);
                return await connection.QueryAsync<Procesados>(@"Select Id, Delivery, Channel ,Prefix ,IdClient ,Client ,Address ,Location 
                                                            ,Province ,Country ,ZipCode ,StoreLocation ,Plant ,Email ,Phone ,Currier 
                                                            ,ReceptionDate ,File_Sony ,CurrierDateProcess   from Pedido_cab 
                                                            where CurrierDateProcess Is null and  format (ReceptionDate,'dd/MM/yyyy') = @f", new { f }); //and  format (ReceptionDate,'dd/MM/yyyy') = @f
            }
            */

        }

        public override async Task<bool> Eliminar(Servicios modelo)
        {
            return Convert.ToBoolean(contextoSingleton.Database.ExecuteSqlRaw($"EliminarServicio {modelo.Id}"));
        }

        public async Task<bool> Guardar(Servicios modelo)
        {
            return Convert.ToBoolean(contextoSingleton.Database.ExecuteSqlRaw($"GuardarServicio '{modelo.Id}', '{modelo.Nombre}', '{modelo.Activo}'"));
        }
    }
}
