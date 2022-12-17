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
    public class RolesController : Controller
    {
        private readonly RolesServices _services;

        public RolesController()
        {
            _services = new RolesServices();
        }

        [HttpGet]
        [Route("BuscarRoles")]
        public async Task<List<Roles>> BuscarRoles()
        {
            return await _services.BuscarLista();
        }

        [HttpPost]
        [Route("GuardarRol")]
        public async Task<bool> GuardarRol(Roles rol)
        {
            return await _services.Guardar(rol);
        }

        [HttpPost]
        [Route("EliminarRol")]
        public async Task<bool> EliminarRol(Roles rol)
        {
            return await _services.Eliminar(rol);
        }

    }
}
