using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        //public ProductosController(IHttpClientFactory httpClient) => _httpClient = httpClient

        public ServiciosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult ServiciosAddPartial([FromBody] Servicios servicios)
        {
            var serviciosViewModel = new ServiciosViewModel();

            if (servicios != null)
            {
                serviciosViewModel = servicios;

            }
            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", serviciosViewModel);
        }

        public IActionResult GuardarServicio(Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var servicios = baseApi.PostToApi("Servicios/GuardarServicio", servicio, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }

        public IActionResult EliminarServicio([FromBody] Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = baseApi.PostToApi("Servicios/EliminarServicio", servicio, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }
    }
}
