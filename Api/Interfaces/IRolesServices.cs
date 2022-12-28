using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IRolesServices
    {
        Task<List<Roles>> BuscarLista();
        Task<bool> Guardar(RolesDto rolesDto);
        Task<bool> Eliminar(RolesDto rolesDto);
    }
}
