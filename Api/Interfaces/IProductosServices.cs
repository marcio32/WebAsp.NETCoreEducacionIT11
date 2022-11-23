using Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosServices
    {
        Task<List<Productos>> BuscarLista();
        Task<List<Productos>> Guardar();
        Task<List<Productos>> Eliminar();
    }
}
