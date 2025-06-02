using BOLSA_VALORES.Data;
using BOLSA_VALORES.Models;
using BOLSA_VALORES.Repositories.Implementaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOLSA_VALORES.Forms
{
    public partial class ReportePortafolioForm : Form
    {
        private Usuario usuario;
        private PortafolioRepository portafolioRepo = new PortafolioRepository();


        public ReportePortafolioForm(int usuarioId)
        {
            InitializeComponent();
            var connection = DatabaseConnection.Instance.GetConnection();

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var usuarioRepo = new UsuarioRepository(connection, null);
            this.usuario = usuarioRepo.ObtenerPorID(usuarioId);
        }



        private void ReportePortafolioForm_Load(object sender, EventArgs e)
        {
            var datos = portafolioRepo.ObtenerReportePortafolio(usuario.UsuarioID);
            dgvReporte.DataSource = datos;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}

