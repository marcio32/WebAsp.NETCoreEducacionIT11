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
    public class UsuariosManager : BaseManager<Usuarios>
    {
        public override Task<Usuarios> Buscar()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Usuarios>> BuscarLista()
        {

            var respuesta = await contextoSingleton.Usuarios.Where(x => x.Activo == true).ToListAsync();

            return respuesta;
        }

        public override async Task<bool> Eliminar(Usuarios modelo)
        {
            contextoSingleton.Entry(modelo).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(modelo).State = EntityState.Detached;

            return resultado;
        }
    }
}
