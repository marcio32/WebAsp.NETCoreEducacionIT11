using Api.Services;
using Commons.Helpers;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperarCuentaController : Controller
    {
        [HttpPost]
        [Route("GuardarCodigo")]
        public bool GuardarCodigo(LoginDto login)
        {
            var recuperarCuenta = new RecuperarCuentaService();
            var usuario = recuperarCuenta.BuscarUsuario(login);
            if(usuario != null)
            {
                usuario.Codigo = login.Codigo;
                return recuperarCuenta.GuardarCodigo(usuario);
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("CambiarClave")]
        public bool CambiarClave(LoginDto login)
        {
            var recuperarCuenta = new RecuperarCuentaService();
            var usuario = recuperarCuenta.BuscarUsuario(login);
            if(usuario != null)
            {
                usuario.Codigo = null;
                usuario.Clave = EncryptHelper.Encriptar(login.Clave);
                return recuperarCuenta.GuardarCodigo(usuario);
            }
            else
            {
                return false;
            }

        }


    }
}
