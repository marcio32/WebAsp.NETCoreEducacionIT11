using Api.Interfaces;
using Commons.Helpers;
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
            return await _manager.BuscarLista();
        }

        public async Task<bool> Eliminar(Roles rol)
        {
            rol.Activo = false;
            return await _manager.Eliminar(rol);
        }

        public async Task<bool> Guardar(Roles rol)
        {
            return await _manager.Guardar(rol, rol.Id);
        }
    }
}
