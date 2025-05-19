using System.Windows.Forms;

namespace BOLSA_VALORES.Forms
{
    partial class UsuariosForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cmbTipoUsuario;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblTipoUsuario;
        private System.Windows.Forms.Label lblSaldo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbTipoUsuario = new System.Windows.Forms.ComboBox();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblTipoUsuario = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();

            this.dgvUsuarios.Location = new System.Drawing.Point(30, 200);
            this.dgvUsuarios.Size = new System.Drawing.Size(500, 200);
            this.dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.CellClick += new DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);

            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(30, 30);
            this.txtNombre.Location = new System.Drawing.Point(120, 30);
            this.txtNombre.Size = new System.Drawing.Size(200, 22);

            this.lblTipoUsuario.Text = "Tipo Usuario:";
            this.lblTipoUsuario.Location = new System.Drawing.Point(30, 70);
            this.cmbTipoUsuario.Location = new System.Drawing.Point(120, 70);
            this.cmbTipoUsuario.Size = new System.Drawing.Size(200, 24);
            this.cmbTipoUsuario.Items.AddRange(new object[] { "Administrador", "Inversor" });

            this.lblSaldo.Text = "Saldo:";
            this.lblSaldo.Location = new System.Drawing.Point(30, 110);
            this.txtSaldo.Location = new System.Drawing.Point(120, 110);
            this.txtSaldo.Size = new System.Drawing.Size(200, 22);

            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Location = new System.Drawing.Point(350, 30);
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);

            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Location = new System.Drawing.Point(350, 70);
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(350, 110);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Location = new System.Drawing.Point(350, 150);
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            this.ClientSize = new System.Drawing.Size(580, 430);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.cmbTipoUsuario);
            this.Controls.Add(this.txtSaldo);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblTipoUsuario);
            this.Controls.Add(this.lblSaldo);
            this.Text = "Gestión de Usuarios";

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
