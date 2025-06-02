using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;
using System.Data.SqlClient;



namespace BOLSA_VALORES.Forms
{
    public partial class ManageAccionesForm : Form
    {
        private AccionRepository accionRepository;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public ManageAccionesForm(SqlConnection connection, SqlTransaction transaction)
        {
            InitializeComponent();
            _connection = connection;
            _transaction = transaction;
            accionRepository = new AccionRepository(_connection, _transaction);
        }

        private void ManageAccionesForm_Load(object sender, EventArgs e)
        {
            CargarAcciones();
        }

        private void CargarAcciones()
        {
            try
            {
                List<Accion> acciones = accionRepository.ObtenerTodas();
                dgvAcciones.DataSource = acciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar acciones: " + ex.Message);
            }
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar acción: " + ex.Message);
            }
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar acción: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAcciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una acción para eliminar.");
                return;
            }

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar acción: " + ex.Message);
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
