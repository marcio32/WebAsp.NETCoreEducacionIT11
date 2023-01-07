using Data.Base;
using Data.Dto;
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

        public IActionResult ServiciosAddPartial([FromBody] ServiciosDto serviciosDto)
        {
            var serviciosViewModel = new ServiciosViewModel();

            if (serviciosDto != null)
            {
                serviciosViewModel = serviciosDto;

            }
            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", serviciosViewModel);
        }

        public IActionResult GuardarServicio(ServiciosDto serviciosDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var servicios = baseApi.PostToApi("Servicios/GuardarServicio", serviciosDto, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }

        public async Task<IActionResult> SincronizarServicio()
        {
            var servicioDto = new ServiciosDto{ Activo = true };
            var oClient = new MiSoap.Service1Client();
            servicioDto.Nombre = oClient.GetServicios();
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            var servicios = await baseApi.PostToApi("Servicios/GuardarServicio", servicioDto, token);
            var resultado = servicios as OkObjectResult;
            return Ok(resultado.Value.ToString());
        }

        public IActionResult EliminarServicio([FromBody] ServiciosDto serviciosDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = baseApi.PostToApi("Servicios/EliminarServicio", serviciosDto, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }
    }
}
