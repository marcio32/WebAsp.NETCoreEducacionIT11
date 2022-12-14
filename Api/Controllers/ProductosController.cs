using Api.Services;
using Data.Dto;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private readonly ProductosServices _services;

        public ProductosController()
        {
            _services = new ProductosServices();
        }

        [HttpGet]
        [Route("BuscarProductos")]
        public async Task<List<Productos>> BuscarProductos()
        {
            return await _services.BuscarLista();
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public async Task<bool> GuardarProducto(ProductosDto productoDto)
        {
            return await _services.Guardar(productoDto);
        }

        [HttpPost]
        [Route("EliminarProducto")]
        public async Task<bool> EliminarProducto(ProductosDto productoDto)
        {
            return await _services.Eliminar(productoDto);
        }

    }
}
