using Api.Services;
using Data.Dto;
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
        public async Task<bool> GuardarUsuario(UsuariosDto usuarioDto)
        {
            return await _services.Guardar(usuarioDto);
        }

        [Authorize]
        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<bool> EliminarUsuario(UsuariosDto usuarioDto)
        {
            return await _services.Eliminar(usuarioDto);
        }

    }
}
