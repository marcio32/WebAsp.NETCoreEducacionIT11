using Api.Services;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosServices _services;

        public UsuariosController()
        {
            _services = new UsuariosServices();
        }

        [HttpGet]
        [Route("BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            return await _services.BuscarLista();
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public async Task<bool> GuardarUsuario(Usuarios usuario)
        {
            return await _services.Guardar(usuario);
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<bool> EliminarUsuario(Usuarios usuario)
        {
            return await _services.Eliminar(usuario);
        }

    }
}
