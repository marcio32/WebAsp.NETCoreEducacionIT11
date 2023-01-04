using Api.Interfaces;
using Commons.Helpers;
using Data.Dto;
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

        public async Task<bool> Eliminar(ServiciosDto servicioDto)
        {
            var servicio = new Servicios();
            servicio = servicioDto;
            servicio.Activo = false;
            return await _manager.Eliminar(servicio);
        }

        public async Task<bool> Guardar(ServiciosDto servicioDto)
        {
            var servicio = new Servicios();
            servicio = servicioDto;
            return await _manager.Guardar(servicio);
        }
    }
}
