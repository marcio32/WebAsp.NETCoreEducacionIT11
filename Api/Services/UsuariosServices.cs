﻿using Api.Interfaces;
using Commons.Helpers;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Mvc;

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
                var buscarLista = await _manager.BuscarLista();

                foreach (var item in buscarLista)
                {
                    item.Clave = EncryptHelper.Desencriptar(item.Clave);
                }

                return buscarLista;


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
                var buscarLista = _manager.BuscarUsuarioRepetido(usuario);

                if (buscarLista != null)
                {
                    return false;
                }

                usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
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
