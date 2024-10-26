namespace CierreDeCajas.Presentacion
{
    partial class FrmPrestamos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbIdMovimiento = new System.Windows.Forms.Label();
            this.lbIdPrestamo = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvAdelantos = new System.Windows.Forms.DataGridView();
            this.rbMensajero = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbTrabajadores = new System.Windows.Forms.RadioButton();
            this.lbxTrabajadores = new System.Windows.Forms.ListBox();
            this.PanelTitulo = new Guna.UI2.WinForms.Guna2Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdelantos)).BeginInit();
            this.PanelTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbIdMovimiento);
            this.panel1.Controls.Add(this.lbIdPrestamo);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.txtObservaciones);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cbConceptos);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.rbMensajero);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtValor);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbTrabajadores);
            this.panel1.Controls.Add(this.lbxTrabajadores);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(92, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1217, 561);
            this.panel1.TabIndex = 0;
            // 
            // lbIdMovimiento
            // 
            this.lbIdMovimiento.AutoSize = true;
            this.lbIdMovimiento.Location = new System.Drawing.Point(267, 9);
            this.lbIdMovimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIdMovimiento.Name = "lbIdMovimiento";
            this.lbIdMovimiento.Size = new System.Drawing.Size(79, 29);
            this.lbIdMovimiento.TabIndex = 22;
            this.lbIdMovimiento.Text = "label1";
            this.lbIdMovimiento.Visible = false;
            // 
            // lbIdPrestamo
            // 
            this.lbIdPrestamo.AutoSize = true;
            this.lbIdPrestamo.Location = new System.Drawing.Point(124, 9);
            this.lbIdPrestamo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIdPrestamo.Name = "lbIdPrestamo";
            this.lbIdPrestamo.Size = new System.Drawing.Size(79, 29);
            this.lbIdPrestamo.TabIndex = 21;
            this.lbIdPrestamo.Text = "label1";
            this.lbIdPrestamo.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoEllipsis = true;
            this.btnGuardar.BackColor = System.Drawing.Color.Gold;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(880, 214);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(184, 50);
            this.btnGuardar.TabIndex = 20;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuardar_KeyDown);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtObservaciones.Location = new System.Drawing.Point(805, 98);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(320, 89);
            this.txtObservaciones.TabIndex = 19;
            this.txtObservaciones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObservaciones_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(608, 98);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 29);
            this.label8.TabIndex = 18;
            this.label8.Text = "Observaciones:";
            // 
            // cbConceptos
            // 
            this.cbConceptos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbConceptos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbConceptos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbConceptos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(805, 49);
            this.cbConceptos.Margin = new System.Windows.Forms.Padding(4);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(320, 33);
            this.cbConceptos.TabIndex = 15;
            this.cbConceptos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbConceptos_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvAdelantos);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(15, 300);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1192, 250);
            this.panel3.TabIndex = 13;
            // 
            // dgvAdelantos
            // 
            this.dgvAdelantos.AllowUserToAddRows = false;
            this.dgvAdelantos.AllowUserToDeleteRows = false;
            this.dgvAdelantos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdelantos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvAdelantos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdelantos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdelantos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvAdelantos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdelantos.Location = new System.Drawing.Point(0, 0);
            this.dgvAdelantos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAdelantos.Name = "dgvAdelantos";
            this.dgvAdelantos.RowHeadersWidth = 51;
            this.dgvAdelantos.Size = new System.Drawing.Size(1192, 250);
            this.dgvAdelantos.TabIndex = 0;
            // 
            // rbMensajero
            // 
            this.rbMensajero.AutoSize = true;
            this.rbMensajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMensajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMensajero.ForeColor = System.Drawing.Color.White;
            this.rbMensajero.Location = new System.Drawing.Point(129, 42);
            this.rbMensajero.Margin = new System.Windows.Forms.Padding(4);
            this.rbMensajero.Name = "rbMensajero";
            this.rbMensajero.Size = new System.Drawing.Size(125, 29);
            this.rbMensajero.TabIndex = 11;
            this.rbMensajero.TabStop = true;
            this.rbMensajero.Text = "Mensajero";
            this.rbMensajero.UseVisualStyleBackColor = true;
            this.rbMensajero.CheckedChanged += new System.EventHandler(this.rbMensajero_CheckedChanged);
            this.rbMensajero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbMensajero_KeyDown);
            this.rbMensajero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbMensajero_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(608, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 29);
            this.label4.TabIndex = 8;
            this.label4.Text = "Concepto:";
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValor.Location = new System.Drawing.Point(129, 208);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(435, 30);
            this.txtValor.TabIndex = 7;
            this.txtValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValor_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(31, 208);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Valor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario:";
            // 
            // rbTrabajadores
            // 
            this.rbTrabajadores.AutoSize = true;
            this.rbTrabajadores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTrabajadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrabajadores.ForeColor = System.Drawing.Color.White;
            this.rbTrabajadores.Location = new System.Drawing.Point(272, 42);
            this.rbTrabajadores.Margin = new System.Windows.Forms.Padding(4);
            this.rbTrabajadores.Name = "rbTrabajadores";
            this.rbTrabajadores.Size = new System.Drawing.Size(128, 29);
            this.rbTrabajadores.TabIndex = 3;
            this.rbTrabajadores.TabStop = true;
            this.rbTrabajadores.Text = "Trabajador";
            this.rbTrabajadores.UseVisualStyleBackColor = true;
            this.rbTrabajadores.CheckedChanged += new System.EventHandler(this.rbTrabajadores_CheckedChanged);
            // 
            // lbxTrabajadores
            // 
            this.lbxTrabajadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbxTrabajadores.FormattingEnabled = true;
            this.lbxTrabajadores.ItemHeight = 25;
            this.lbxTrabajadores.Location = new System.Drawing.Point(129, 79);
            this.lbxTrabajadores.Margin = new System.Windows.Forms.Padding(4);
            this.lbxTrabajadores.Name = "lbxTrabajadores";
            this.lbxTrabajadores.Size = new System.Drawing.Size(435, 79);
            this.lbxTrabajadores.TabIndex = 2;
            this.lbxTrabajadores.SelectedIndexChanged += new System.EventHandler(this.lbTrabajadores_SelectedIndexChanged);
            this.lbxTrabajadores.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxTrabajadores_KeyDown);
            // 
            // PanelTitulo
            // 
            this.PanelTitulo.Controls.Add(this.label6);
            this.PanelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelTitulo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelTitulo.Name = "PanelTitulo";
            this.PanelTitulo.Size = new System.Drawing.Size(1379, 52);
            this.PanelTitulo.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(178)))), ((int)(((byte)(231)))));
            this.label6.Location = new System.Drawing.Point(557, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 46);
            this.label6.TabIndex = 11;
            this.label6.Text = "ADELANTOS";
            // 
            // FrmPrestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1379, 646);
            this.Controls.Add(this.PanelTitulo);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPrestamos";
            this.Text = "FrmPrestamos";
            this.Load += new System.EventHandler(this.FrmPrestamos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdelantos)).EndInit();
            this.PanelTitulo.ResumeLayout(false);
            this.PanelTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxTrabajadores;
        private System.Windows.Forms.RadioButton rbTrabajadores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbMensajero;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvAdelantos;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbConceptos;
        private System.Windows.Forms.Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Panel PanelTitulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbIdPrestamo;
        private System.Windows.Forms.Label lbIdMovimiento;
    }
}