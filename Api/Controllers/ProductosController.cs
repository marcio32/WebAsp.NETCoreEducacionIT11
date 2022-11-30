using Api.Services;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        public async Task<bool> GuardarProducto(Productos producto)
        {
            return await _services.Guardar(producto);
        }

        [HttpPost]
        [Route("EliminarProducto")]
        public async Task<bool> EliminarProducto(Productos producto)
        {
            return await _services.Eliminar(producto);
        }

    }
}
