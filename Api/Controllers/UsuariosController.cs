using Api.Services;
using Data.Dtos;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            return await _services.BuscarLista();
        }

        [Authorize]
        [HttpPost]
        [Route("GuardarUsuario")]
        public async Task<bool> GuardarUsuario(Usuarios usuario)
        {
            return await _services.Guardar(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<bool> CrearUsuario(CrearUsuarioDto usuario)
        {
            var usuarios = new Usuarios();
            usuarios = usuario;
            return await _services.Guardar(usuarios);
        }

        [Authorize]
        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<bool> EliminarUsuario(Usuarios usuario)
        {
            return await _services.Eliminar(usuario);
        }

    }
}
