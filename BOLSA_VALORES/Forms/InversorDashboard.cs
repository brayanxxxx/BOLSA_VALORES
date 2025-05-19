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

        public InversorDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.Load += InversorDashboard_Load;
        }

        private void InversorDashboard_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Bienvenido, {usuario.Nombre}";
            CargarAcciones();
        }

        private void CargarAcciones()
        {
            var repo = new AccionRepository();
            var lista = repo.ObtenerTodas(); 
            dgvAcciones.DataSource = lista;
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una acción para comprar.");
                return;
            }

            var accion = (Accion)dgvAcciones.SelectedRows[0].DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            MessageBox.Show($"Comprando {cantidad} de {accion.Nombre}");
          
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una acción para vender.");
                return;
            }

            var accion = (Accion)dgvAcciones.SelectedRows[0].DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            MessageBox.Show($"Vendiendo {cantidad} de {accion.Nombre}");
            
        }
    }
}
