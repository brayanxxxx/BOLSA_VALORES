using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOLSA_VALORES.Data;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;
using BOLSA_VALORES.Services;


namespace BOLSA_VALORES.Forms
{
    public partial class InversorDashboard : Form
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private Usuario usuario;


        private AccionRepository accionRepo;
        private UsuarioRepository usuarioRepo;
        private TransaccionRepository transaccionRepo;

        public InversorDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;

            _connection = DatabaseConnection.Instance.GetConnection();
            _transaction = _connection.BeginTransaction();

            accionRepo = new AccionRepository(_connection, _transaction);
            usuarioRepo = new UsuarioRepository(_connection, _transaction);
            transaccionRepo = new TransaccionRepository(_connection, _transaction);
        }

        private void InversorDashboard_Load(object sender, EventArgs e)
        {
            RefrescarDatos();
            timerSimulacion.Start();
        }

        private void RefrescarDatos()
        {
            usuario = usuarioRepo.ObtenerPorID(usuario.UsuarioID);
            lblSaldo.Text = $"Saldo: {usuario.Saldo:C}";

            var acciones = accionRepo.ObtenerTodas();
            dgvAcciones.DataSource = acciones;
        }

        private void TimerSimulacion_Tick(object sender, EventArgs e)
        {

            accionRepo.SimularCambios(mensaje =>
            {
              
                MessageBox.Show(mensaje, "Notificación de cambio importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });

            RefrescarDatos();
        }


        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una acción primero.");
                return;
            }

            var accion = (Accion)dgvAcciones.CurrentRow.DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            decimal totalPrecio = accion.PrecioActual * cantidad;
            if (usuario.Saldo < totalPrecio)
            {
                MessageBox.Show("No tienes saldo suficiente para comprar esta cantidad.");
                return;
            }

            try
            {
                transaccionRepo.RegistrarTransaccion(new Transaccion
                {
                    UsuarioID = usuario.UsuarioID,
                    AccionID = accion.AccionID,
                    TipoTransaccion = "Compra",
                    Cantidad = cantidad,
                    Precio = accion.PrecioActual,
                    Fecha = DateTime.Now
                });

                MessageBox.Show("Compra realizada con éxito.");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Fondos insuficientes"))
                {
                    MessageBox.Show("No tienes saldo suficiente para realizar esta compra.");
                }
                else
                {
                    MessageBox.Show("Error al realizar la compra: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }

            RefrescarDatos();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una acción primero.");
                return;
            }

            var accion = (Accion)dgvAcciones.CurrentRow.DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            int cantidadDisponible = transaccionRepo.ObtenerCantidadAccionUsuario(usuario.UsuarioID, accion.AccionID);
            if (cantidad > cantidadDisponible)
            {
                MessageBox.Show("No tienes suficiente cantidad para vender.");
                return;
            }

            try
            {
                transaccionRepo.RegistrarTransaccion(new Transaccion
                {
                    UsuarioID = usuario.UsuarioID,
                    AccionID = accion.AccionID,
                    TipoTransaccion = "Venta",
                    Cantidad = cantidad,
                    Precio = accion.PrecioActual,
                    Fecha = DateTime.Now
                });

                MessageBox.Show("Venta realizada con éxito.");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("No tiene suficientes acciones para vender"))
                {
                    MessageBox.Show("No tienes suficiente cantidad para vender.");
                }
                else
                {
                    MessageBox.Show("Error al realizar la venta: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }

            RefrescarDatos();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            HistorialTransaccionesForm historialForm = new HistorialTransaccionesForm(usuario, _connection, _transaction);
            historialForm.ShowDialog();
        }



        private void btnVerPortafolio_Click(object sender, EventArgs e)
        {
            ReportePortafolioForm reporteForm = new ReportePortafolioForm(usuario.UsuarioID);
            reporteForm.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }


    }
}
