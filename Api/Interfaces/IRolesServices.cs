using Data.Entities;

namespace Api.Interfaces
{
    public interface IRolesServices
    {
        Task<List<Roles>> BuscarLista();
        Task<bool> Guardar(Roles roles);
        Task<bool> Eliminar(Roles roles);
    }
}
