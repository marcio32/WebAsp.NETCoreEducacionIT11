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
            try
            {
                var respuesta = _manager.BuscarUsuario(usuario);
                return respuesta;
            }
            catch(Exception ex)
            {
                GenerateLogHelper.LogError(ex, "RecuperarCuentaService", "BuscarUsuario");
                return null;
            }
        }

        public bool GuardarCodigo(Usuarios usuario)
        {
            try
            {
                var respuesta = _manager.Guardar(usuario, usuario.Id);
                return respuesta.Result;
            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "RecuperarCuentaService", "GuardarCodigo");
                return false;
            }

        }
    }
}
