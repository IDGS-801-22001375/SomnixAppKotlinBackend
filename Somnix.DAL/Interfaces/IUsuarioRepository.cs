using Somnix.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DAL.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmail(string email);
        Task<Usuario?> ObtenerPorId(string id);
        Task<Usuario> Registrar(Usuario usuario);
        Task<Usuario> Actualizar(string id, Usuario usuario);
    }
}
