using Api.Services;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : Controller
    {
        private readonly ServiciosServices _services;

        public ServiciosController()
        {
            _services = new ServiciosServices();
        }

        [HttpGet]
        [Route("BuscarServicios")]
        public async Task<List<Servicios>> BuscarServicios()
        {
            return await _services.BuscarLista();
        }

        [HttpPost]
        [Route("GuardarServicio")]
        public async Task<bool> GuardarServicio(Servicios servicio)
        {
            return await _services.Guardar(servicio);
        }

        [HttpPost]
        [Route("EliminarServicio")]
        public async Task<bool> EliminarServicio(Servicios servicio)
        {
            return await _services.Eliminar(servicio);
        }

    }
}
