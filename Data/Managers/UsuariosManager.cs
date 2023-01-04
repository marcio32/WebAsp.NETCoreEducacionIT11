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
            return await contextoSingleton.Usuarios.Where(x => x.Activo == true).Include(x=> x.Roles).ToListAsync();
        }

        public async Task<Usuarios> BuscarUsuario(string mail)
        {
            return await contextoSingleton.Usuarios.Where(x => x.Activo == true && x.Mail == mail).Include(x => x.Roles).FirstOrDefaultAsync();
        }

        public async Task<Usuarios> BuscarUsuario(string mail, string clave)
        {
            return await contextoSingleton.Usuarios.Where(x => x.Activo == true && x.Mail == mail && x.Clave == clave).Include(x => x.Roles).FirstOrDefaultAsync();
        }

        public async Task<Usuarios> BuscarUsuarioRepetido(Usuarios modelo)
        {

            return await contextoSingleton.Usuarios.FirstOrDefaultAsync(x => x.Mail == modelo.Mail);
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
            return await contextoSingleton.Usuarios.FirstOrDefaultAsync(x => x.Mail == usuario.Mail && x.Activo == true);
        }
    }
}
