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
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;


namespace BOLSA_VALORES.Forms
{
    public partial class InversorDashboard : Form
    {
        private Usuario usuario;
        private AccionRepository accionRepo = new AccionRepository();
        private TransaccionRepository transaccionRepo = new TransaccionRepository();
        private UsuarioRepository usuarioRepo = new UsuarioRepository();

        public InversorDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void InversorDashboard_Load(object sender, EventArgs e)
        {
            RefrescarDatos();
        }

        private void RefrescarDatos()
        {
            usuario = usuarioRepo.ObtenerPorID(usuario.UsuarioID);
            lblSaldo.Text = $"Saldo: {usuario.Saldo:C}";
            var acciones = accionRepo.ObtenerTodas();
            dgvAcciones.DataSource = acciones;
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

            // Opcional: solo validación rápida previa, pero no actualizar saldo aquí.
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

            // Opcional: validación rápida, pero no actualizar saldo aquí.
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

    }
}
