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
         
        }
    }
}

