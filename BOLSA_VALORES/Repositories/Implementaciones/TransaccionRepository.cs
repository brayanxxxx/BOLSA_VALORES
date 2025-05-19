using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOLSA_VALORES.Models;
using System.Data.SqlClient;
using BOLSA_VALORES.Data;

namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class TransaccionRepository
    {
        public void RegistrarTransaccion(Transaccion transaccion)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            string query = @"INSERT INTO Transacciones 
                            (UsuarioID, AccionID, TipoTransaccion, Cantidad, Precio, Fecha)
                            VALUES (@UsuarioID, @AccionID, @TipoTransaccion, @Cantidad, @Precio, @Fecha)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", transaccion.UsuarioID);
                cmd.Parameters.AddWithValue("@AccionID", transaccion.AccionID);
                cmd.Parameters.AddWithValue("@TipoTransaccion", transaccion.TipoTransaccion);
                cmd.Parameters.AddWithValue("@Cantidad", transaccion.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", transaccion.Precio);
                cmd.Parameters.AddWithValue("@Fecha", transaccion.Fecha);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Transaccion> ObtenerPorUsuario(int usuarioId)
        {
            var lista = new List<Transaccion>();
            var connection = DatabaseConnection.Instance.GetConnection();
            string query = "SELECT * FROM Transacciones WHERE UsuarioID = @UsuarioID ORDER BY Fecha DESC";

            using (SqlCommand cmd = new SqlCommand(query, connection))
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
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}