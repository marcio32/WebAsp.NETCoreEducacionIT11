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
            _manager =  new ProductosManager();
        }
        public async Task<List<Productos>> BuscarLista()
        {
            try
            {
                return await _manager.BuscarLista();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Productos>> Eliminar()
        {
            throw new NotImplementedException();
        }

        public Task<List<Productos>> Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
