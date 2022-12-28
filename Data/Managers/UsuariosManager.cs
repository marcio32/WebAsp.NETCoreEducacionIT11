using Data.Base;
using Data.Dto;
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

            var respuesta = await contextoSingleton.Usuarios.Where(x => x.Activo == true).Include(x=> x.Roles).ToListAsync();

            return respuesta;
        }

        public async Task<Usuarios> BuscarUsuarioRepetido(Usuarios modelo)
        {

            var respuesta = contextoSingleton.Usuarios.FirstOrDefault(x => x.Mail == modelo.Mail);

            return respuesta;
        }


        public override async Task<bool> Eliminar(Usuarios modelo)
        {
            contextoSingleton.Entry(modelo).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(modelo).State = EntityState.Detached;

            return resultado;
        }

        public async Task<Usuarios> BuscarUsuarioGoogleAsync(LoginDto usuario)
        {
            var result = await contextoSingleton.Usuarios.FirstOrDefaultAsync(x => x.Mail == usuario.Mail && x.Activo == true);
            return result;
        }
    }
}
