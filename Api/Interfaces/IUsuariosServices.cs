using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosServices
    {
        Task<List<Usuarios>> BuscarLista();
        Task<bool> Guardar(UsuariosDto usuarioDto);
        Task<bool> Eliminar(UsuariosDto usuarioDto);
    }
}
