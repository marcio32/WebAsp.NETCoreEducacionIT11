using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IRecuperarCuentaServices
    {
        public bool GuardarCodigo(Usuarios usuario);
        Usuarios BuscarUsuario(LoginDto usuario);
    }
}
