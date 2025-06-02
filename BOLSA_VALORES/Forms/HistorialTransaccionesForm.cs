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
    public partial class HistorialTransaccionesForm : Form
    {
        private int usuarioId;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private TransaccionRepository transaccionRepo;


        public HistorialTransaccionesForm(Usuario usuario, SqlConnection connection, SqlTransaction transaction)
        {
            InitializeComponent();
            this.usuarioId = usuario.UsuarioID;
            this._connection = connection;
            this._transaction = transaction;
            transaccionRepo = new TransaccionRepository(_connection, _transaction);
        }
        private void HistorialTransaccionesForm_Load(object sender, EventArgs e)
        {
            var historial = transaccionRepo.ObtenerPorUsuario(usuarioId);
            dgvHistorial.DataSource = historial;

            dgvHistorial.Columns["TransaccionID"].Visible = false;
            dgvHistorial.Columns["UsuarioID"].Visible = false;
            dgvHistorial.Columns["AccionID"].Visible = false;

            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
