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
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public UsuarioRepository(SqlConnection connection, SqlTransaction transaction = null)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction; // puede ser null
        }

        public Usuario AutenticarUsuario(string username, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_AutenticarUsuario", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

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
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar usuario: " + ex.Message, ex);
            }
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerTodosUsuarios", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

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
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los usuarios: " + ex.Message, ex);
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_AgregarUsuario", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Saldo", usuario.Saldo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message, ex);
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarUsuario", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

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
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message, ex);
            }
        }

        public Usuario ObtenerPorID(int usuarioID)
        {
            Usuario usuario = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioPorID", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

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
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por ID: " + ex.Message, ex);
            }
        }

        public void ActualizarSaldo(int usuarioID, decimal nuevoSaldo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarSaldo", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmd.Parameters.AddWithValue("@Saldo", nuevoSaldo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar saldo: " + ex.Message, ex);
            }
        }

        public void EliminarUsuario(int usuarioId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_EliminarUsuario", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message, ex);
            }
        }
    }
}
