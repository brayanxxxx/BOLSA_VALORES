using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOLSA_VALORES.Data;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Interfaces;
using System.Data.SqlClient;



namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario AutenticarUsuario(string username, string password)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_AutenticarUsuario", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            UsuarioID = (int)reader["UsuarioID"],
                            Nombre = reader["Nombre"].ToString(),
                            TipoUsuario = reader["TipoUsuario"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Saldo = Convert.ToDecimal(reader["Saldo"])
                        };
                    }
                }
            }
            return null;
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            var connection = DatabaseConnection.Instance.GetConnection();

            using (SqlCommand cmd = new SqlCommand("SP_ObtenerTodosUsuarios", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            UsuarioID = (int)reader["UsuarioID"],
                            Nombre = reader["Nombre"].ToString(),
                            TipoUsuario = reader["TipoUsuario"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Saldo = Convert.ToDecimal(reader["Saldo"])
                        });
                    }
                }
            }
            return lista;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_AgregarUsuario", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Saldo", usuario.Saldo);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_ActualizarUsuario", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Saldo", usuario.Saldo);
                cmd.ExecuteNonQuery();
            }
        }

        public Usuario ObtenerPorID(int usuarioID)
        {
            Usuario usuario = null;
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioPorID", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            UsuarioID = (int)reader["UsuarioID"],
                            Nombre = reader["Nombre"].ToString(),
                            TipoUsuario = reader["TipoUsuario"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Saldo = Convert.ToDecimal(reader["Saldo"])
                        };
                    }
                }
            }

            return usuario;
        }

        public void ActualizarSaldo(int usuarioID, decimal nuevoSaldo)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_ActualizarSaldo", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                cmd.Parameters.AddWithValue("@Saldo", nuevoSaldo);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int usuarioId)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            using (SqlCommand cmd = new SqlCommand("SP_EliminarUsuario", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
