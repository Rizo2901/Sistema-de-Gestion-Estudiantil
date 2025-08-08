using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Helpers
{
    public static class UsuarioHelper
    {
        public static string GenerarNombreUsuario(EntidadBasicaResponse entidad)
        {
            var nombre = entidad.Nombre?.Trim().ToLower() ?? "";
            var a1 = entidad.Apellido1?.Trim().ToLower() ?? "";
            var a2 = entidad.Apellido2?.Trim().ToLower() ?? "";
            var cedula = entidad.Cedula?.Replace("-", "") ?? "";

            if (cedula.Length < 4)
                throw new Exception("Cédula inválida para generar usuario.");

            return $"{nombre.FirstOrDefault()}{a1.FirstOrDefault()}{a2.FirstOrDefault()}{cedula[^4..]}";
        }

        public static string GenerarContrasenaSegura()
        {
            var caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz123456789@$#%!?";
            var rnd = new Random();
            return new string(Enumerable.Range(0, 12).Select(_ => caracteres[rnd.Next(caracteres.Length)]).ToArray());
        }
    }


}
