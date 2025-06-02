using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace BOLSA_VALORES.Forms
{
    public partial class UsuariosForm : Form
    {
        private UsuarioRepository usuarioRepo;
        private int? usuarioSeleccionadoId = null;

        public UsuariosForm(SqlConnection connection, SqlTransaction transaction)
        {
            InitializeComponent();

            try
            {
                usuarioRepo = new UsuarioRepository(connection, transaction);
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar UsuariosForm: " + ex.Message);
                this.Close();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                var lista = usuarioRepo.ObtenerTodos();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            cmbTipoUsuario.SelectedIndex = -1;
            txtUsername.Clear();
            txtPassword.Clear();
            txtSaldo.Clear();
            usuarioSeleccionadoId = null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
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

                if (!decimal.TryParse(txtSaldo.Text, out decimal saldo))
                {
                    MessageBox.Show("Saldo inválido.");
                    return;
                }

                var usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    TipoUsuario = cmbTipoUsuario.Text,
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Saldo = saldo
                };

                usuarioRepo.AgregarUsuario(usuario);
                CargarUsuarios();
                LimpiarCampos();
                MessageBox.Show("Usuario agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar usuario: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionadoId == null)
                {
                    MessageBox.Show("Selecciona un usuario primero.");
                    return;
                }

                if (!decimal.TryParse(txtSaldo.Text, out decimal saldo))
                {
                    MessageBox.Show("Saldo inválido.");
                    return;
                }

                var usuario = new Usuario
                {
                    UsuarioID = usuarioSeleccionadoId.Value,
                    Nombre = txtNombre.Text.Trim(),
                    TipoUsuario = cmbTipoUsuario.Text,
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Saldo = saldo
                };

                usuarioRepo.ActualizarUsuario(usuario);
                CargarUsuarios();
                LimpiarCampos();
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar usuario: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionadoId == null)
                {
                    MessageBox.Show("Selecciona un usuario primero.");
                    return;
                }

                var confirm = MessageBox.Show("¿Seguro que deseas eliminar al usuario?", "Confirmación", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    usuarioRepo.EliminarUsuario(usuarioSeleccionadoId.Value);
                    CargarUsuarios();
                    LimpiarCampos();
                    MessageBox.Show("Usuario eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message);
            }
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
