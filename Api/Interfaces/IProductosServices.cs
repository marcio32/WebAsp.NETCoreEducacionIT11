using Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosServices
    {
        Task<List<Productos>> BuscarLista();
        Task<bool> Guardar(Productos productos);
        Task<bool> Eliminar(Productos productos);
    }
}
