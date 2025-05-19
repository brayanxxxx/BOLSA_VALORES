using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BOLSA_VALORES.Forms
{
    public partial class UsuariosForm : Form
    {
        private UsuarioRepository usuarioRepo;
        private int? usuarioSeleccionadoId = null;

        public UsuariosForm()
        {
            InitializeComponent();
            usuarioRepo = new UsuarioRepository();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var lista = usuarioRepo.ObtenerTodos();
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = lista;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            cmbTipoUsuario.SelectedIndex = -1;
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtSaldo.Text = "";
            usuarioSeleccionadoId = null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(cmbTipoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtSaldo.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            var usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                TipoUsuario = cmbTipoUsuario.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Saldo = decimal.Parse(txtSaldo.Text)
            };
            usuarioRepo.AgregarUsuario(usuario);
            CargarUsuarios();
            LimpiarCampos();
            MessageBox.Show("Usuario agregado correctamente.");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            var usuario = new Usuario
            {
                UsuarioID = usuarioSeleccionadoId.Value,
                Nombre = txtNombre.Text,
                TipoUsuario = cmbTipoUsuario.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Saldo = decimal.Parse(txtSaldo.Text)
            };
            usuarioRepo.ActualizarUsuario(usuario);
            CargarUsuarios();
            LimpiarCampos();
            MessageBox.Show("Usuario actualizado correctamente.");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            usuarioRepo.EliminarUsuario(usuarioSeleccionadoId.Value);
            CargarUsuarios();
            LimpiarCampos();
            MessageBox.Show("Usuario eliminado correctamente.");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvUsuarios.Rows[e.RowIndex];
                usuarioSeleccionadoId = Convert.ToInt32(fila.Cells["UsuarioID"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                cmbTipoUsuario.Text = fila.Cells["TipoUsuario"].Value.ToString();
                txtUsername.Text = fila.Cells["Username"].Value.ToString();
                txtPassword.Text = fila.Cells["Password"].Value.ToString();
                txtSaldo.Text = fila.Cells["Saldo"].Value.ToString();
            }
        }
    }
}
