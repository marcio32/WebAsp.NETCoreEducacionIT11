using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosServices
    {
        Task<List<Productos>> BuscarLista();
        Task<bool> Guardar(ProductosDto productos);
        Task<bool> Eliminar(ProductosDto productos);
    }
}
