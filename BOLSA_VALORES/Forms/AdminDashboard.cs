using BOLSA_VALORES.Models;
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
    public partial class AdminDashboard : Form
    {
        private Usuario usuario;

        public AdminDashboard(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }
    }
}
