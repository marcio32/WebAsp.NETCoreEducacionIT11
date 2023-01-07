using Data.Base;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        //public ProductosController(IHttpClientFactory httpClient) => _httpClient = httpClient

        public ProductosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public IActionResult Productos()
        {
            return View();
        }


        public IActionResult ProductosAddPartial([FromBody] ProductosDto productoDto)
        {
            var productosViewModel = new ProductosViewModel();
           
            if(productoDto != null)
            {
                productosViewModel = productoDto;

            }
            return PartialView("~/Views/Productos/Partial/ProductosAddPartial.cshtml", productosViewModel);
        }

        public  IActionResult GuardarProducto(ProductosDto productoDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);

            if(productoDto.Imagen_Archivo != null && productoDto.Imagen_Archivo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    productoDto.Imagen_Archivo.CopyTo(ms);
                    var imagenBytes = ms.ToArray();
                    productoDto.Imagen = Convert.ToBase64String(imagenBytes);
                }
            }
            productoDto.Imagen_Archivo = null;

            var productos = baseApi.PostToApi("Productos/GuardarProducto", productoDto, token);
            return View("~/Views/Productos/Productos.cshtml");
        }

        public IActionResult EliminarProducto([FromBody] ProductosDto productoDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var productos = baseApi.PostToApi("Productos/EliminarProducto", productoDto, token);
            return View("~/Views/Productos/Productos.cshtml");
        }
    }
}
