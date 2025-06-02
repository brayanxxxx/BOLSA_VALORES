using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Data;
using BOLSA_VALORES.Repositories.Interfaces;

namespace BOLSA_VALORES.Repositories.Implementaciones
{
    public class AccionRepository : IAccionRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public AccionRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<Accion> ObtenerTodas()
        {
            try
            {
                var lista = new List<Accion>();
                string query = "SELECT AccionID, Simbolo, Nombre, Sector, PrecioActual, VariacionDiaria FROM Acciones";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var accion = new Accion
                        {
                            AccionID = (int)reader["AccionID"],
                            Simbolo = reader["Simbolo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Sector = reader["Sector"] == DBNull.Value ? null : reader["Sector"].ToString(),
                            PrecioActual = Convert.ToDecimal(reader["PrecioActual"]),
                            VariacionDiaria = Convert.ToDecimal(reader["VariacionDiaria"])
                        };

                        lista.Add(accion);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las acciones.", ex);
            }
        }

        public void ActualizarPreciosConSP()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarPrecioAccion", _connection, _transaction))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar precios con procedimiento almacenado.", ex);
            }
        }

        public void AgregarAccion(Accion accion)
        {
            try
            {
                string query = "INSERT INTO Acciones (Simbolo, Nombre, Sector, PrecioActual, VariacionDiaria) " +
                               "VALUES (@Simbolo, @Nombre, @Sector, @PrecioActual, @VariacionDiaria)";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("@Simbolo", accion.Simbolo);
                    cmd.Parameters.AddWithValue("@Nombre", accion.Nombre);

                    if (accion.Sector == null)
                        cmd.Parameters.AddWithValue("@Sector", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Sector", accion.Sector);

                    cmd.Parameters.AddWithValue("@PrecioActual", accion.PrecioActual);
                    cmd.Parameters.AddWithValue("@VariacionDiaria", accion.VariacionDiaria);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar acción.", ex);
            }
        }

        public void ActualizarAccion(Accion accion)
        {
            try
            {
                string query = "UPDATE Acciones SET Simbolo = @Simbolo, Nombre = @Nombre, Sector = @Sector, " +
                               "PrecioActual = @PrecioActual, VariacionDiaria = @VariacionDiaria WHERE AccionID = @AccionID";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("@Simbolo", accion.Simbolo);
                    cmd.Parameters.AddWithValue("@Nombre", accion.Nombre);

                    if (accion.Sector == null)
                        cmd.Parameters.AddWithValue("@Sector", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Sector", accion.Sector);

                    cmd.Parameters.AddWithValue("@PrecioActual", accion.PrecioActual);
                    cmd.Parameters.AddWithValue("@VariacionDiaria", accion.VariacionDiaria);
                    cmd.Parameters.AddWithValue("@AccionID", accion.AccionID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar acción.", ex);
            }
        }

        public void EliminarAccion(int accionId)
        {
            try
            {
                string query = "DELETE FROM Acciones WHERE AccionID = @AccionID";

                using (SqlCommand cmd = new SqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("@AccionID", accionId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar acción.", ex);
            }
        }

        public void SimularCambios(Action<string> notificarCambioImportante = null)
        {
            try
            {
                var acciones = ObtenerTodas();
                Random rnd = new Random();
                decimal umbral = 0.04m;

                foreach (var accion in acciones)
                {
                    decimal precioAnterior = accion.PrecioActual;
                    decimal cambio = (decimal)(rnd.NextDouble() * 0.1 - 0.05); 
                    accion.PrecioActual += precioAnterior * cambio;

                    if (accion.PrecioActual < 1)
                        accion.PrecioActual = 1;

                    ActualizarAccion(accion);

                    decimal variacionReal = Math.Abs((accion.PrecioActual - precioAnterior) / precioAnterior);
                    if (variacionReal >= umbral)
                    {
                        string mensaje = $"⚠ La acción {accion.Simbolo} ha cambiado un {Math.Round(variacionReal * 100, 2)}%.";
                        notificarCambioImportante?.Invoke(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al simular cambios en acciones.", ex);
            }
        }
    }
}
