using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOLSA_VALORES.Repositories.Implementaciones;
using BOLSA_VALORES.Models;


namespace BOLSA_VALORES.Forms
{
    public partial class LoginForm : Form
    {
        private UsuarioRepository usuarioRepo;

        public LoginForm()
        {
            InitializeComponent();
            usuarioRepo = new UsuarioRepository();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingresa todos los datos.");
                return;
            }

            Usuario usuario = usuarioRepo.AutenticarUsuario(username, password);

            if (usuario == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
                return;
            }

            MessageBox.Show($"Bienvenido, {usuario.Nombre} ({usuario.TipoUsuario})");

            if (usuario.TipoUsuario == "Administrador")
            {
                AdminDashboard adminForm = new AdminDashboard(usuario);
                adminForm.Show();
            }
            else if (usuario.TipoUsuario == "Inversor")
            {
                InversorDashboard inversorForm = new InversorDashboard(usuario);
                inversorForm.Show();
            }

            this.Hide(); 
        }
    }
}
