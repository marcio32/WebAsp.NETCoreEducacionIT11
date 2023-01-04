using Data.Dto;
using Data.Entities;

namespace Web.Interfaces
{
	public interface IUsuariosServices
	{
		Task<Usuarios> buscarUsuario(LoginDto login);
	}
}
