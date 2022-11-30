using Data.Base;
using Data.Entities;
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
        public IActionResult Usuarios()
        {
            return View();
        }

        public async Task<IActionResult> UsuariosAddPartial([FromBody] Usuarios usuarios)
        {
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.GetToApi("Roles/BuscarRoles", "");
            var usuariosViewModel = new UsuariosViewModel();
            var resultadoRoles = roles as OkObjectResult;

            if (usuarios != null)
            {
                usuariosViewModel = usuarios;

            }

            if(resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<Roles>>(resultadoRoles.Value.ToString());
                var listaItemsRoles = new List<SelectListItem>();
                foreach (var item in listaRoles)
                {
                    listaItemsRoles.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString() });
                }
                usuariosViewModel.Lista_Roles = listaItemsRoles;
            }
            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuariosViewModel);
        }

        public IActionResult GuardarUsuario(Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);

            var usuarios = baseApi.PostToApi("Usuarios/GuardarUsuario", usuario, "");
            return View("~/Views/Usuarios/Usuarios.cshtml");
        }

        public IActionResult EliminarUsuario([FromBody] Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);
            var usuarios = baseApi.PostToApi("Usuarios/EliminarUsuario", usuario, "");
            return View("~/Views/Usuarios/Usuarios.cshtml");
        }
    }
}
