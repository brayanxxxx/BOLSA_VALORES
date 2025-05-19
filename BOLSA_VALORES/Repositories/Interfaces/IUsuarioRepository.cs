using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BOLSA_VALORES.Models;

namespace BOLSA_VALORES.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario AutenticarUsuario(string username, string password);
        List<Usuario> ObtenerTodos();
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int usuarioId);
    }
}
