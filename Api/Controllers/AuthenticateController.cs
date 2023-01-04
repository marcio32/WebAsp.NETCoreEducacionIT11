using Api.Services;
using Commons.Helpers;
using Data;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsuariosServices _services;
        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
            _services = new UsuariosServices();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            loginDto.Clave = EncryptHelper.Encriptar(loginDto.Clave);
            var validarUsuario = await _services.BuscarUsuario(loginDto.Mail, loginDto.Clave);
            if (validarUsuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre)
                };

                var token = CrearToken(claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);

            }
            else
            {
                return Unauthorized();
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> LoginGoogle(LoginDto loginDto)
        {
            var validarUsuario = await _services.BuscarUsuario(loginDto.Mail);
            if (validarUsuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre)
                };

                var token = CrearToken(claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);

            }
            else
            {
                return Unauthorized();
            }
        }

        private JwtSecurityToken CrearToken(List<Claim> autorizar)
        {
            var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Firma"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(24),
                claims: autorizar,
                signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
