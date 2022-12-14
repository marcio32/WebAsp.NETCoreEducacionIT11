using Data.Base;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;
        public LoginController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _smtpClient = new SmtpClient();
        }

        public IActionResult Login()
        {
            if (TempData["ErrorLogin"] != null)
            {
                ViewBag.ErrorLogin = TempData["ErrorLogin"].ToString();
            }
            return View();
        }

        public IActionResult CrearCuenta()
        {
            return View();
        }

        public IActionResult OlvidoClave()
        {
            return View("~/Views/Login/OlvidoClave.cshtml");
        }

        public IActionResult RecuperarCuenta()
        {
            return View("~/Views/Login/RecuperarCuenta.cshtml");
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
                TempData["ErrorLogin"] = "La contraseña o el mail no coinciden";
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
    
        public async Task<IActionResult> EnviarMail(LoginDto login)
        {
            var guid = Guid.NewGuid();
            var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(numeros.Substring(0, 6));
            var random = new Random(seed);
            var codigo = random.Next(000000, 999999);

            login.Codigo = codigo;

            var baseApi = new BaseApi(_httpClient);
            var respuesta = await baseApi.PostToApi("RecuperarCuenta/GuardarCodigo", login, "");
            var resultadoLogin = respuesta as OkObjectResult;

            if (resultadoLogin != null && resultadoLogin.Value.ToString() == "true")
            {
                MailMessage mail = new();

                string cuerpoMail = CuerpoMailLogin(codigo);

                mail.From = new MailAddress(_configuration["ConfiguracionMail:Usuario"]);
                mail.To.Add(login.Mail);
                mail.Subject = "Codigo Recuperacion";
                mail.Body = cuerpoMail;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                _smtpClient.Host = _configuration["ConfiguracionMail:DireccionServidor"];
                _smtpClient.Port = int.Parse(_configuration["ConfiguracionMail:Puerto"]);
                _smtpClient.EnableSsl = bool.Parse(_configuration["ConfiguracionMail:Ssl"]);
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(_configuration["ConfiguracionMail:Usuario"], _configuration["ConfiguracionMail:Clave"]);
                _smtpClient.Send(mail);
                return RedirectToAction("RecuperarCuenta", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }

        private static string CuerpoMailLogin(int codigo)
        {
            var separacion = "<br>";
            var mensaje = "<strong>A continuacion se mostrara un codigo que debera ingresar en la web del curso de Educacion IT</strong>";
            mensaje += $" <strong>{codigo}</strong> {separacion}";
            return mensaje;
        }
    

        public async Task<IActionResult> CambiarClave(LoginDto login)
        {
            var baseApi = new BaseApi(_httpClient);
            var respuesta = await baseApi.PostToApi("RecuperarCuenta/CambiarClave", login, "");
            var resultadoLogin = respuesta as OkObjectResult;

            if(resultadoLogin != null && resultadoLogin.Value.ToString() == "true")
            {
                TempData["ErrorLogin"] = "Se cambio correctamente la clave";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["ErrorLogin"] = "El codigo ingresado no coincide con el enviado al mail, por favor genere uno nuevo";
                return RedirectToAction("Login", "Login");

            }
        }
    }
}
