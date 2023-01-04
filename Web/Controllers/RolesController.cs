using Data.Base;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        //public ProductosController(IHttpClientFactory httpClient) => _httpClient = httpClient

        public RolesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult RolesAddPartial([FromBody] RolesDto rolesDto)
        {
            var rolesViewModel = new RolesViewModel();

            if (rolesDto != null)
            {
                rolesViewModel = rolesDto;

            }
            return PartialView("~/Views/Roles/Partial/RolesAddPartial.cshtml", rolesViewModel);
        }

        public IActionResult GuardarRol(RolesDto rolesDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = baseApi.PostToApi("Roles/GuardarRol", rolesDto, token);
            return View("~/Views/Roles/Roles.cshtml");
        }

        public IActionResult EliminarRol([FromBody] RolesDto rolesDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = baseApi.PostToApi("Roles/EliminarRol", rolesDto, token);
            return View("~/Views/Roles/Roles.cshtml");
        }
    }
}
