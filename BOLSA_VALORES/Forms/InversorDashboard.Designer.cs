namespace BOLSA_VALORES.Forms
{
    partial class InversorDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvAcciones;
        private System.Windows.Forms.Button btnComprar;
        private System.Windows.Forms.Button btnVender;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblTituloAcciones;
        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.Timer timerSimulacion;
        private System.Windows.Forms.Button btnVerPortafolio;
        private System.Windows.Forms.Button btnCerrarSesion; 

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvAcciones = new System.Windows.Forms.DataGridView();
            this.btnComprar = new System.Windows.Forms.Button();
            this.btnVender = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblTituloAcciones = new System.Windows.Forms.Label();
            this.panelDerecho = new System.Windows.Forms.Panel();
            this.btnVerPortafolio = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.timerSimulacion = new System.Windows.Forms.Timer(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.panelDerecho.SuspendLayout();
            this.SuspendLayout();

            // 
            // dgvAcciones
            // 
            this.dgvAcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcciones.Location = new System.Drawing.Point(12, 50);
            this.dgvAcciones.MultiSelect = false;
            this.dgvAcciones.Name = "dgvAcciones";
            this.dgvAcciones.ReadOnly = true;
            this.dgvAcciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcciones.Size = new System.Drawing.Size(460, 380);
            this.dgvAcciones.TabIndex = 0;

            // 
            // btnComprar
            // 
            this.btnComprar.Location = new System.Drawing.Point(20, 100);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(170, 35);
            this.btnComprar.TabIndex = 3;
            this.btnComprar.Text = "Comprar";
            this.btnComprar.UseVisualStyleBackColor = true;
            this.btnComprar.Click += new System.EventHandler(this.btnComprar_Click);

            // 
            // btnVender
            // 
            this.btnVender.Location = new System.Drawing.Point(20, 140);
            this.btnVender.Name = "btnVender";
            this.btnVender.Size = new System.Drawing.Size(170, 35);
            this.btnVender.TabIndex = 4;
            this.btnVender.Text = "Vender";
            this.btnVender.UseVisualStyleBackColor = true;
            this.btnVender.Click += new System.EventHandler(this.btnVender_Click);

            // 
            // btnHistorial
            // 
            this.btnHistorial.Location = new System.Drawing.Point(20, 180);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(170, 35);
            this.btnHistorial.TabIndex = 5;
            this.btnHistorial.Text = "Ver Historial";
            this.btnHistorial.UseVisualStyleBackColor = true;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);

            // 
            // btnVerPortafolio
            // 
            this.btnVerPortafolio.Location = new System.Drawing.Point(20, 220);
            this.btnVerPortafolio.Name = "btnVerPortafolio";
            this.btnVerPortafolio.Size = new System.Drawing.Size(170, 35);
            this.btnVerPortafolio.TabIndex = 6;
            this.btnVerPortafolio.Text = "Ver Portafolio";
            this.btnVerPortafolio.UseVisualStyleBackColor = true;
            this.btnVerPortafolio.Click += new System.EventHandler(this.btnVerPortafolio_Click);

            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(20, 260);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(170, 35);
            this.btnCerrarSesion.TabIndex = 7;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(90, 58);
            this.nudCantidad.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(100, 20);
            this.nudCantidad.TabIndex = 2;
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCantidad.Location = new System.Drawing.Point(20, 60);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(64, 19);
            this.lblCantidad.TabIndex = 1;
            this.lblCantidad.Text = "Cantidad";

            // 
            // lblSaldo
            // 
            this.lblSaldo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSaldo.Location = new System.Drawing.Point(15, 10);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(250, 30);
            this.lblSaldo.TabIndex = 0;
            this.lblSaldo.Text = "Saldo: $0.00";

            // 
            // lblTituloAcciones
            // 
            this.lblTituloAcciones.AutoSize = true;
            this.lblTituloAcciones.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloAcciones.Location = new System.Drawing.Point(12, 15);
            this.lblTituloAcciones.Name = "lblTituloAcciones";
            this.lblTituloAcciones.Size = new System.Drawing.Size(172, 21);
            this.lblTituloAcciones.TabIndex = 0;
            this.lblTituloAcciones.Text = "Acciones Disponibles";

            // 
            // panelDerecho
            // 
            this.panelDerecho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDerecho.Controls.Add(this.lblSaldo);
            this.panelDerecho.Controls.Add(this.lblCantidad);
            this.panelDerecho.Controls.Add(this.nudCantidad);
            this.panelDerecho.Controls.Add(this.btnComprar);
            this.panelDerecho.Controls.Add(this.btnVender);
            this.panelDerecho.Controls.Add(this.btnHistorial);
            this.panelDerecho.Controls.Add(this.btnVerPortafolio);
            this.panelDerecho.Controls.Add(this.btnCerrarSesion); 
            this.panelDerecho.Location = new System.Drawing.Point(490, 50);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Size = new System.Drawing.Size(220, 310); 
            this.panelDerecho.TabIndex = 1;

            // 
            // timerSimulacion
            // 
            this.timerSimulacion.Interval = 10000;
            this.timerSimulacion.Tick += new System.EventHandler(this.TimerSimulacion_Tick);

            // 
            // InversorDashboard
            // 
            this.ClientSize = new System.Drawing.Size(730, 450);
            this.Controls.Add(this.lblTituloAcciones);
            this.Controls.Add(this.dgvAcciones);
            this.Controls.Add(this.panelDerecho);
            this.Name = "InversorDashboard";
            this.Text = "Dashboard Inversor";
            this.Load += new System.EventHandler(this.InversorDashboard_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.panelDerecho.ResumeLayout(false);
            this.panelDerecho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
