using Data.Base;
using Data.Dto;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class RecuperarCuentaManager : BaseManager<Usuarios>
    {
        public Usuarios? BuscarUsuario(LoginDto usuario)
        {
            if(usuario.Clave != null)
            {
                return contextoSingleton.Usuarios.Where(x => x.Codigo == usuario.Codigo).FirstOrDefault();

            }
            else
            {
                return contextoSingleton.Usuarios.Where(x => x.Mail == usuario.Mail).FirstOrDefault();

            }
        }

        public override Task<Usuarios> Buscar()
        {
            throw new NotImplementedException();
        }

        public override Task<List<Usuarios>> BuscarLista()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Eliminar(Usuarios modelo)
        {
            throw new NotImplementedException();
        }
    }
}
