using Api.Interfaces;
using Commons.Helpers;
using Data.Dto;
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

        public async Task<bool> Eliminar(RolesDto rolDto)
        {
            var rol = new Roles();
            rol = rolDto;
            rol.Activo = false;
            return await _manager.Eliminar(rol);
        }

        public async Task<bool> Guardar(RolesDto rolDto)
        {
            var rol = new Roles();
            rol = rolDto;
            return await _manager.Guardar(rol, rol.Id);
        }
    }
}
