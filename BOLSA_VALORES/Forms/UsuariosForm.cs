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
            dgvUsuarios.DataSource = lista;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            cmbTipoUsuario.SelectedIndex = -1;
            txtSaldo.Text = "";
            usuarioSeleccionadoId = null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                TipoUsuario = cmbTipoUsuario.Text,
                Saldo = decimal.Parse(txtSaldo.Text)
            };
            usuarioRepo.AgregarUsuario(usuario);
            CargarUsuarios();
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null) return;

            var usuario = new Usuario
            {
                UsuarioID = usuarioSeleccionadoId.Value,
                Nombre = txtNombre.Text,
                TipoUsuario = cmbTipoUsuario.Text,
                Saldo = decimal.Parse(txtSaldo.Text)
            };
            usuarioRepo.ActualizarUsuario(usuario);
            CargarUsuarios();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null) return;

            usuarioRepo.EliminarUsuario(usuarioSeleccionadoId.Value);
            CargarUsuarios();
            LimpiarCampos();
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
                txtSaldo.Text = fila.Cells["Saldo"].Value.ToString();
            }
        }
    }
}

