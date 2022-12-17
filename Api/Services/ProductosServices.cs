using Api.Interfaces;
using Commons.Helpers;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class ProductosServices : IProductosServices
    {
        private readonly ProductosManager _manager;

        public ProductosServices()
        {
            _manager = new ProductosManager();
        }
        public async Task<List<Productos>> BuscarLista()
        {
            return await _manager.BuscarLista();
        }

        public async Task<bool> Eliminar(Productos producto)
        {
            producto.Activo = false;
            return await _manager.Eliminar(producto);
        }

        public async Task<bool> Guardar(Productos producto)
        {
            return await _manager.Guardar(producto, producto.Id);
        }
    }
}
