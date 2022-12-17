using Api.Interfaces;
using Commons.Helpers;
using Data.Dto;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class RecuperarCuentaService : IRecuperarCuentaServices
    {
        private readonly RecuperarCuentaManager _manager;

        public RecuperarCuentaService()
        {
            _manager = new RecuperarCuentaManager();
        }

        public Usuarios BuscarUsuario(LoginDto usuario)
        {
            return _manager.BuscarUsuario(usuario);
        }

        public bool GuardarCodigo(Usuarios usuario)
        {
            return _manager.Guardar(usuario, usuario.Id).Result;
        }
    }
}
