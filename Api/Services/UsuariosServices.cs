using Api.Interfaces;
using Commons.Helpers;
using Data.Dto;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly UsuariosManager _manager;

        public UsuariosServices()
        {
            _manager = new UsuariosManager();
        }
        public async Task<List<Usuarios>> BuscarLista()
        {
            var buscarLista = await _manager.BuscarLista();

            foreach (var item in buscarLista)
            {
                item.Clave = EncryptHelper.Desencriptar(item.Clave);
            }

            return buscarLista;
        }

        public async Task<Usuarios> BuscarUsuario(string mail)
        {
            return await _manager.BuscarUsuario(mail);
        }
        public async Task<Usuarios> BuscarUsuario(string mail, string clave)
        {
            return await _manager.BuscarUsuario(mail, clave);
        }

        public async Task<bool> Eliminar(UsuariosDto usuarioDto)
        {
            var usuario = new Usuarios();
            usuario = usuarioDto;
            usuario.Activo = false;
            return await _manager.Eliminar(usuario);
        }

        public async Task<bool> Guardar(UsuariosDto usuarioDto)
        {
            var usuario = new Usuarios();
            usuario = usuarioDto;
            var buscarLista = _manager.BuscarUsuarioRepetido(usuario);

            if (buscarLista.Result != null && usuario.Id == 0)
            {
                return false;
            }

            usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
            return await _manager.Guardar(usuario, usuario.Id);
        }
    }
}
