using Api.Interfaces;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class RolesServices : IRolesServices
    {
        private readonly RolesManager _manager;

        public RolesServices()
        {
            _manager = new RolesManager();
        }
        public async Task<List<Roles>> BuscarLista()
        {
            try
            {
                return await _manager.BuscarLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Eliminar(Roles rol)
        {
            rol.Activo = false;
            return await _manager.Eliminar(rol);
        }

        public async Task<bool> Guardar(Roles rol)
        {
            try
            {
                return await _manager.Guardar(rol, rol.Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
