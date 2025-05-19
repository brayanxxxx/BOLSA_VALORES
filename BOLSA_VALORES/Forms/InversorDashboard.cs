using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public InversorDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void InversorDashboard_Load(object sender, EventArgs e)
        {
            lblSaldo.Text = $"Saldo: ${usuario.Saldo:F2}";
            CargarAcciones();
        }

        private void CargarAcciones()
        {
            var acciones = accionRepo.ObtenerTodas();
            dgvAcciones.DataSource = acciones;

            if (dgvAcciones.Columns.Contains("AccionID"))
                dgvAcciones.Columns["AccionID"].Visible = false;

            
        }


        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una acción para comprar.");
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor que cero.");
                return;
            }

            var accion = (Accion)dgvAcciones.CurrentRow.DataBoundItem;
            decimal totalCompra = accion.PrecioActual * cantidad;

            if (usuario.Saldo < totalCompra)
            {
                MessageBox.Show("Saldo insuficiente para realizar la compra.");
                return;
            }

            usuario.Saldo -= totalCompra;
            ActualizarSaldoUsuario();

            Transaccion compra = new Transaccion
            {
                UsuarioID = usuario.UsuarioID,
                AccionID = accion.AccionID,
                TipoTransaccion = "Compra",
                Cantidad = cantidad,
                Precio = accion.PrecioActual,
                Fecha = DateTime.Now
            };
            transaccionRepo.RegistrarTransaccion(compra);

            MessageBox.Show("Compra realizada con éxito.");
            lblSaldo.Text = $"Saldo: ${usuario.Saldo:F2}";

            nudCantidad.Value = 1; // Reset a 1
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una acción para vender.");
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor que cero.");
                return;
            }

            var accion = (Accion)dgvAcciones.CurrentRow.DataBoundItem;
            decimal totalVenta = accion.PrecioActual * cantidad;

            usuario.Saldo += totalVenta;
            ActualizarSaldoUsuario();

            Transaccion venta = new Transaccion
            {
                UsuarioID = usuario.UsuarioID,
                AccionID = accion.AccionID,
                TipoTransaccion = "Venta",
                Cantidad = cantidad,
                Precio = accion.PrecioActual,
                Fecha = DateTime.Now
            };
            transaccionRepo.RegistrarTransaccion(venta);

            MessageBox.Show("Venta realizada con éxito.");
            lblSaldo.Text = $"Saldo: ${usuario.Saldo:F2}";

            nudCantidad.Value = 1;
        }

        private void ActualizarSaldoUsuario()
        {
            var usuarioRepo = new UsuarioRepository();
            usuarioRepo.ActualizarUsuario(usuario);
        }
    }
}

