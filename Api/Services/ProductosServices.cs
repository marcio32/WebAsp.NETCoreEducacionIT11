using Api.Interfaces;
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
            try
            {
                return await _manager.BuscarLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Productos>> Eliminar(Productos producto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Productos>> Guardar(Productos producto)
        {
            try
            {
                var resultado = await _manager.Guardar(producto, producto.Id);
                return await _manager.BuscarLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
