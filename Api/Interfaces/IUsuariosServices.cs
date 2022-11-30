using Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosServices
    {
        Task<List<Usuarios>> BuscarLista();
        Task<bool> Guardar(Usuarios usuarios);
        Task<bool> Eliminar(Usuarios usuarios);
    }
}
