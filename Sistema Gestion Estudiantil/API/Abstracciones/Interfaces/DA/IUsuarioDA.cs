using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<IEnumerable<UsuariosResponse>> ObtenerUsuarios();

        Task<UsuariosResponse> ObtenerUsuarioPorId(Guid Id);

        Task<Guid> CrearUsuario(UsuariosResponse usuario);

        Task<Guid> EditarUsuario(Guid Id, UsuariosRequest usuario);

        Task<Guid> EliminarUsuario(Guid Id);


    }
}
