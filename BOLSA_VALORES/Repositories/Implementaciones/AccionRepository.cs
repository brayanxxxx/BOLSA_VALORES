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

            string query = "SELECT AccionID, Simbolo, Nombre, Sector, PrecioActual, VariacionDiaria FROM Acciones";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var accion = new Accion
                    {
                        AccionID = (int)reader["AccionID"],
                        Simbolo = reader["Simbolo"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Sector = reader["Sector"].ToString(),
                        PrecioActual = Convert.ToDecimal(reader["PrecioActual"]),
                        VariacionDiaria = Convert.ToDecimal(reader["VariacionDiaria"])
                    };

                    lista.Add(accion);
                }
            }

            return lista;
        }

        public void ActualizarPreciosConSP()
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            using (SqlCommand cmd = new SqlCommand("SP_ActualizarPrecioAccion", connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }


        public void AgregarAccion(Accion accion)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "INSERT INTO Acciones (Simbolo, Nombre, Sector, PrecioActual, VariacionDiaria) " +
                           "VALUES (@Simbolo, @Nombre, @Sector, @PrecioActual, @VariacionDiaria)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Simbolo", accion.Simbolo);
                cmd.Parameters.AddWithValue("@Nombre", accion.Nombre);
                cmd.Parameters.AddWithValue("@Sector", accion.Sector ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PrecioActual", accion.PrecioActual);
                cmd.Parameters.AddWithValue("@VariacionDiaria", accion.VariacionDiaria);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarAccion(Accion accion)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "UPDATE Acciones SET Simbolo = @Simbolo, Nombre = @Nombre, Sector = @Sector, " +
                           "PrecioActual = @PrecioActual, VariacionDiaria = @VariacionDiaria WHERE AccionID = @AccionID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Simbolo", accion.Simbolo);
                cmd.Parameters.AddWithValue("@Nombre", accion.Nombre);
                cmd.Parameters.AddWithValue("@Sector", accion.Sector ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PrecioActual", accion.PrecioActual);
                cmd.Parameters.AddWithValue("@VariacionDiaria", accion.VariacionDiaria);
                cmd.Parameters.AddWithValue("@AccionID", accion.AccionID);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarAccion(int accionId)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            string query = "DELETE FROM Acciones WHERE AccionID = @AccionID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@AccionID", accionId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
