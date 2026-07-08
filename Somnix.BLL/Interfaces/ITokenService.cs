using Somnix.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}
