using Api.Interfaces;
using Commons.Helpers;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly UsuariosManager _manager;

        public UsuariosServices()
        {
            _manager = new UsuariosManager();
        }
        public async Task<List<Usuarios>> BuscarLista()
        {
            try
            {
                return await _manager.BuscarLista();
            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "Usuarios", "BuscarLista");
                throw ex;
            }
        }

        public async Task<bool> Eliminar(Usuarios usuario)
        {
            usuario.Activo = false;
            return await _manager.Eliminar(usuario);
        }

        public async Task<bool> Guardar(Usuarios usuario)
        {
            try
            {
                return await _manager.Guardar(usuario, usuario.Id);

            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "Usuarios", "Guardar");
                throw ex;
            }
        }
    }
}
