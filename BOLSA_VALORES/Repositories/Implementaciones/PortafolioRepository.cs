using BOLSA_VALORES.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOLSA_VALORES.Models;

namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class PortafolioRepository
    {
        private readonly SqlConnection connection;

        public PortafolioRepository()
        {
            connection = DatabaseConnection.Instance.GetConnection();
        }

        public List<PortafolioItem> ObtenerReportePortafolio(int usuarioId)
        {
            var lista = new List<PortafolioItem>();
            using (var cmd = new SqlCommand("SP_GenerarReportePortafolio", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PortafolioItem
                        {
                            Simbolo = reader["Simbolo"].ToString(),
                            NombreAccion = reader["NombreAccion"].ToString(),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            PrecioActual = Convert.ToDecimal(reader["PrecioActual"]),
                            ValorInvertido = Convert.ToDecimal(reader["ValorInvertido"]),
                            ValorActual = Convert.ToDecimal(reader["ValorActual"]),
                            Ganancia = Convert.ToDecimal(reader["Ganancia"])
                        });
                    }
                }
            }
            return lista;
        }

        public bool UsuarioPoseeAccion(int usuarioId, int accionId)
        {
            var connection = DatabaseConnection.Instance.GetConnection();
            string query = "SELECT COUNT(*) FROM Portafolio WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);
                cmd.Parameters.AddWithValue("@AccionID", accionId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

    }

}
