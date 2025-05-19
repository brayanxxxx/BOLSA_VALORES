namespace BOLSA_VALORES.Forms
{
    partial class InversorDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblSaldoTexto;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.DataGridView dgvAcciones;
        private System.Windows.Forms.Button btnComprar;
        private System.Windows.Forms.Button btnVender;
        private System.Windows.Forms.Label lblEstado;

        private System.Windows.Forms.NumericUpDown nudCantidad;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblSaldoTexto = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.dgvAcciones = new System.Windows.Forms.DataGridView();
            this.btnComprar = new System.Windows.Forms.Button();
            this.btnVender = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).BeginInit();
            this.SuspendLayout();

            // lblBienvenida
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(20, 20);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(180, 25);
            this.lblBienvenida.Text = "Bienvenido, Inversor";

            // lblSaldoTexto
            this.lblSaldoTexto.AutoSize = true;
            this.lblSaldoTexto.Location = new System.Drawing.Point(22, 60);
            this.lblSaldoTexto.Name = "lblSaldoTexto";
            this.lblSaldoTexto.Size = new System.Drawing.Size(85, 15);
            this.lblSaldoTexto.Text = "Saldo actual: $";

            // lblSaldo
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Location = new System.Drawing.Point(110, 60);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(28, 15);
            this.lblSaldo.Text = "0.00";

            // dgvAcciones
            this.dgvAcciones.AllowUserToAddRows = false;
            this.dgvAcciones.AllowUserToDeleteRows = false;
            this.dgvAcciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcciones.Location = new System.Drawing.Point(20, 90);
            this.dgvAcciones.Name = "dgvAcciones";
            this.dgvAcciones.ReadOnly = true;
            this.dgvAcciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcciones.Size = new System.Drawing.Size(600, 200);

            // btnComprar
            this.btnComprar.Location = new System.Drawing.Point(20, 310);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(120, 30);
            this.btnComprar.Text = "Comprar acción";
            this.btnComprar.UseVisualStyleBackColor = true;
            this.btnComprar.Click += new System.EventHandler(this.btnComprar_Click);

            // btnVender
            this.btnVender.Location = new System.Drawing.Point(160, 310);
            this.btnVender.Name = "btnVender";
            this.btnVender.Size = new System.Drawing.Size(120, 30);
            this.btnVender.Text = "Vender acción";
            this.btnVender.UseVisualStyleBackColor = true;
            this.btnVender.Click += new System.EventHandler(this.btnVender_Click);

            // lblEstado
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(20, 360);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 15);

            //nudCantidad
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.nudCantidad.Location = new System.Drawing.Point(20, 270);
            this.nudCantidad.Minimum = 1;
            this.nudCantidad.Maximum = 10000;

            // InversorDashboard
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.lblSaldoTexto);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.dgvAcciones);
            this.Controls.Add(this.btnComprar);
            this.Controls.Add(this.btnVender);
            this.Controls.Add(this.lblEstado);
            this.Name = "InversorDashboard";
            this.Text = "Panel del Inversor";
            this.Load += new System.EventHandler(this.InversorDashboard_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
