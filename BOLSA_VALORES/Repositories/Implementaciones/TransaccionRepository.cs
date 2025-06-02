using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOLSA_VALORES.Models;
using System.Data.SqlClient;
using BOLSA_VALORES.Data;
using System.Data;



namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class TransaccionRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public TransaccionRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void RegistrarTransaccion(Transaccion transaccion)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_RegistrarTransaccion", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UsuarioID", transaccion.UsuarioID);
                    cmd.Parameters.AddWithValue("@AccionID", transaccion.AccionID);
                    cmd.Parameters.AddWithValue("@TipoTransaccion", transaccion.TipoTransaccion);
                    cmd.Parameters.AddWithValue("@Cantidad", transaccion.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", transaccion.Precio);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la transacción: " + ex.Message, ex);
            }
        }

        public List<Transaccion> ObtenerPorUsuario(int usuarioId)
        {
            var lista = new List<Transaccion>();

            try
            {
                string query = "SELECT * FROM Transacciones WHERE UsuarioID = @UsuarioID ORDER BY FechaTransaccion DESC";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transaccion
                            {
                                TransaccionID = (int)reader["TransaccionID"],
                                UsuarioID = (int)reader["UsuarioID"],
                                AccionID = (int)reader["AccionID"],
                                TipoTransaccion = reader["TipoTransaccion"].ToString(),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Precio = Convert.ToDecimal(reader["PrecioUnitario"]),
                                Fecha = Convert.ToDateTime(reader["FechaTransaccion"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener transacciones del usuario: " + ex.Message, ex);
            }

            return lista;
        }

        public int ObtenerCantidadAccionUsuario(int usuarioID, int accionID)
        {
            try
            {
                string query = @"
                    SELECT 
                        ISNULL(SUM(CASE WHEN TipoTransaccion = 'Compra' THEN Cantidad ELSE 0 END), 0) -
                        ISNULL(SUM(CASE WHEN TipoTransaccion = 'Venta' THEN Cantidad ELSE 0 END), 0) AS CantidadDisponible
                    FROM Transacciones
                    WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmd.Parameters.AddWithValue("@AccionID", accionID);

                    object result = cmd.ExecuteScalar();
                    return (result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad disponible de acciones: " + ex.Message, ex);
            }
        }
    }
}
