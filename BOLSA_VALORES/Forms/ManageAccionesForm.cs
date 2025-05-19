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
    public partial class ManageAccionesForm : Form
    {
        private AccionRepository accionRepository;

        public ManageAccionesForm()
        {
            InitializeComponent();
            accionRepository = new AccionRepository();
        }

        private void ManageAccionesForm_Load(object sender, EventArgs e)
        {
            CargarAcciones();
        }

        private void CargarAcciones()
        {
            List<Accion> acciones = accionRepository.ObtenerTodas();
            dgvAcciones.DataSource = acciones;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtSimbolo.Text) ||
                !decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Por favor, ingresa datos válidos.");
                return;
            }

            var nuevaAccion = new Accion
            {
                Nombre = txtNombre.Text.Trim(),
                Simbolo = txtSimbolo.Text.Trim(),
                PrecioActual = precio
            };

            accionRepository.AgregarAccion(nuevaAccion);
            MessageBox.Show("Acción agregada correctamente.");
            CargarAcciones();
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una acción para actualizar.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            int id = (int)dgvAcciones.CurrentRow.Cells["AccionID"].Value;

            var accionActualizada = new Accion
            {
                AccionID = id,
                Nombre = txtNombre.Text.Trim(),
                Simbolo = txtSimbolo.Text.Trim(),
                PrecioActual = precio
            };

            accionRepository.ActualizarAccion(accionActualizada);
            MessageBox.Show("Acción actualizada.");
            CargarAcciones();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una acción para eliminar.");
                return;
            }

            int id = (int)dgvAcciones.CurrentRow.Cells["AccionID"].Value;

            var confirmResult = MessageBox.Show("¿Estás seguro de eliminar esta acción?",
                                     "Confirmar eliminación",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                accionRepository.EliminarAccion(id);
                MessageBox.Show("Acción eliminada.");
                CargarAcciones();
                LimpiarCampos();
            }
        }

        private void dgvAcciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow != null)
            {
                txtNombre.Text = dgvAcciones.CurrentRow.Cells["Nombre"].Value.ToString();
                txtSimbolo.Text = dgvAcciones.CurrentRow.Cells["Simbolo"].Value.ToString();
                txtPrecio.Text = dgvAcciones.CurrentRow.Cells["PrecioActual"].Value.ToString();
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarAcciones();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtSimbolo.Clear();
            txtPrecio.Clear();
        }
    }
}
