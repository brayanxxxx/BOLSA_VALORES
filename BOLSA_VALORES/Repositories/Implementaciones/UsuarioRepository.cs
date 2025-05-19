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
            string query = "SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
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
            string query = "SELECT * FROM Usuarios";

            using (SqlCommand cmd = new SqlCommand(query, connection))
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
            return lista;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            string query = @"INSERT INTO Usuarios (Nombre, TipoUsuario, Username, Password, Saldo)
                             VALUES (@Nombre, @TipoUsuario, @Username, @Password, @Saldo)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
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
            string query = @"UPDATE Usuarios SET Nombre = @Nombre, TipoUsuario = @TipoUsuario,
                             Username = @Username, Password = @Password, Saldo = @Saldo
                             WHERE UsuarioID = @UsuarioID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Saldo", usuario.Saldo);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int usuarioId)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            string query = "DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
