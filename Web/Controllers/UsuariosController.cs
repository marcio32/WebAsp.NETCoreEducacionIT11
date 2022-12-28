using Data.Base;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        //public ProductosController(IHttpClientFactory httpClient) => _httpClient = httpClient

        public UsuariosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public IActionResult Usuarios()
        {
            return View();
        }

        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuariosDto usuarioDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.GetToApi("Roles/BuscarRoles", token);
            var usuariosViewModel = new UsuariosViewModel();
            var resultadoRoles = roles as OkObjectResult;

            if (usuarioDto != null)
            {
                usuariosViewModel = usuarioDto;

            }

            if (resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<RolesDto>>(resultadoRoles.Value.ToString());
                
                var listaItemsRoles = new List<SelectListItem>();
                foreach (var item in listaRoles)
                {
                    listaItemsRoles.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString() });
                }
                usuariosViewModel.Lista_Roles = listaItemsRoles;
            }
            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuariosViewModel);
        }

        public IActionResult GuardarUsuario(UsuariosDto usuarioDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var usuarios = baseApi.PostToApi("Usuarios/GuardarUsuario", usuarioDto, token);

            return View("~/Views/Usuarios/Usuarios.cshtml");
        }

        public IActionResult EliminarUsuario([FromBody] UsuariosDto usuarioDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var usuarios = baseApi.PostToApi("Usuarios/EliminarUsuario", usuarioDto, token);

            return View("~/Views/Usuarios/Usuarios.cshtml");
        }
    }
}
