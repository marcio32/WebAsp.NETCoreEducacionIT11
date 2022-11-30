using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class RolesManager : BaseManager<Roles>
    {
        public override Task<Roles> Buscar()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Roles>> BuscarLista()
        {

            var respuesta = await contextoSingleton.Roles.Where(x => x.Activo == true).ToListAsync();

            return respuesta;
        }

        public override async Task<bool> Eliminar(Roles modelo)
        {
            contextoSingleton.Entry(modelo).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(modelo).State = EntityState.Detached;

            return resultado;
        }
    }
}
