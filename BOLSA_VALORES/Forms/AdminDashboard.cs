using BOLSA_VALORES.Models;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using BOLSA_VALORES.Data;


namespace BOLSA_VALORES.Forms
{
    public partial class AdminDashboard : Form
    {
        private Usuario usuario;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public AdminDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;

            try
            {
                _connection = DatabaseConnection.Instance.GetConnection();
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar conexión: " + ex.Message);
                Close();
            }
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                var usuariosForm = new UsuariosForm(_connection, _transaction);
                usuariosForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir gestión de usuarios: " + ex.Message);
            }
        }

        private void btnGestionarAcciones_Click(object sender, EventArgs e)
        {
            try
            {
                var accionesForm = new ManageAccionesForm(_connection, _transaction);
                accionesForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir gestión de acciones: " + ex.Message);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                _transaction?.Commit(); 
                _connection?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar sesión: " + ex.Message);
            }

            this.Close();
        }
    }
}
