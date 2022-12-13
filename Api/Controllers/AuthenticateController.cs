using Commons.Helpers;
using Data;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private static ApplicationDbContext contextIntance;
        public AuthenticateController()
        {
            contextIntance = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto usuario)
        {
            usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
            var validarUsuario = contextIntance.Usuarios.Include(x=> x.Roles).FirstOrDefault(u => u.Clave == usuario.Clave && u.Mail == usuario.Mail);
            if(validarUsuario != null)
            {
                return Ok(validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);

            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
