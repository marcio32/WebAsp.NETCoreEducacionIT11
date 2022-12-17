using Api.Interfaces;
using Commons.Helpers;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class ServiciosServices : IServiciosServices
    {
        private readonly ServiciosManager _manager;

        public ServiciosServices()
        {
            _manager = new ServiciosManager();
        }
        public async Task<List<Servicios>> BuscarLista()
        {
            return await _manager.BuscarLista();
        }

        public async Task<bool> Eliminar(Servicios rol)
        {
            rol.Activo = false;
            return await _manager.Eliminar(rol);
        }

        public async Task<bool> Guardar(Servicios rol)
        {
            return await _manager.Guardar(rol);
        }
    }
}
