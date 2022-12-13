using Data.Base;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CrearCuenta()
        {
            return View();
        }

        public async Task<IActionResult> Ingresar(LoginDto login)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Authenticate/Login", login, "");
            var resultadoLogin = token as OkObjectResult;

            if (resultadoLogin != null)
            {
                var resultadoSplit = resultadoLogin.Value.ToString().Split(";");
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new(ClaimTypes.Name, resultadoSplit[0]);
                Claim claimRole = new(ClaimTypes.Role, resultadoSplit[1]);
                Claim claimEmail = new(ClaimTypes.Email, resultadoSplit[2]);

                identity.AddClaim(claimNombre);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddDays(1)
                });



                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        public async Task<IActionResult> CrearUsuario(Usuarios usuario)
        {
            usuario.Id_Rol = 2;
            usuario.Activo = true;
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("Usuarios/GuardarUsuario", usuario, "");
            var resultadoLogin = response as OkObjectResult;
            if(resultadoLogin != null && resultadoLogin.Value.ToString() == "true")
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
