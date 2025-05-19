namespace BOLSA_VALORES.Forms
{
    partial class AdminDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnGestionarUsuarios;
        private System.Windows.Forms.Button btnGestionarAcciones;
        private System.Windows.Forms.Button btnCerrarSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnGestionarUsuarios = new System.Windows.Forms.Button();
            this.btnGestionarAcciones = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.Location = new System.Drawing.Point(40, 30);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Size = new System.Drawing.Size(200, 40);
            this.btnGestionarUsuarios.Text = "Gestionar Usuarios";
            this.btnGestionarUsuarios.UseVisualStyleBackColor = true;
            this.btnGestionarUsuarios.Click += new System.EventHandler(this.btnGestionarUsuarios_Click);
            // 
            // btnGestionarAcciones
            // 
            this.btnGestionarAcciones.Location = new System.Drawing.Point(40, 80);
            this.btnGestionarAcciones.Name = "btnGestionarAcciones";
            this.btnGestionarAcciones.Size = new System.Drawing.Size(200, 40);
            this.btnGestionarAcciones.Text = "Gestionar Acciones";
            this.btnGestionarAcciones.UseVisualStyleBackColor = true;
            this.btnGestionarAcciones.Click += new System.EventHandler(this.btnGestionarAcciones_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(40, 130);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(200, 40);
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // 
            // AdminDashboard
            // 
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.btnGestionarUsuarios);
            this.Controls.Add(this.btnGestionarAcciones);
            this.Controls.Add(this.btnCerrarSesion);
            this.Name = "AdminDashboard";
            this.Text = "Panel de Administrador";
            this.ResumeLayout(false);
        }
    }
}
