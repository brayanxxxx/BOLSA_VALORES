using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Data;

namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class AccionRepository
    {
        public List<Accion> ObtenerTodas()
        {
            var lista = new List<Accion>();
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "SELECT * FROM Acciones";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var accion = new Accion
                    {
                        AccionID = (int)reader["AccionID"],
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        CantidadDisponible = (int)reader["CantidadDisponible"]
                    };

                    lista.Add(accion);
                }
            }

            return lista;
        }

        public void ActualizarPrecio(int accionId, decimal nuevoPrecio)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "UPDATE Acciones SET Precio = @Precio WHERE AccionID = @AccionID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Precio", nuevoPrecio);
                cmd.Parameters.AddWithValue("@AccionID", accionId);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCantidad(int accionId, int nuevaCantidad)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "UPDATE Acciones SET CantidadDisponible = @Cantidad WHERE AccionID = @AccionID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Cantidad", nuevaCantidad);
                cmd.Parameters.AddWithValue("@AccionID", accionId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
