namespace CierreDeCajas.Presentacion.Administrativo
{
    partial class FrmTrabajador
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
            this.components = new System.ComponentModel.Container();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbTrabajadores = new System.Windows.Forms.RadioButton();
            this.rbMensajero = new System.Windows.Forms.RadioButton();
            this.rbInactivo = new System.Windows.Forms.RadioButton();
            this.rbActivo = new System.Windows.Forms.RadioButton();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRol = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvTrabajadores = new System.Windows.Forms.DataGridView();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.AccionMovimiento = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse6 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel1.SuspendLayout();
            this.PanelControles.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrabajadores)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.panel1.Controls.Add(this.rbTrabajadores);
            this.panel1.Controls.Add(this.rbMensajero);
            this.panel1.Controls.Add(this.rbInactivo);
            this.panel1.Controls.Add(this.rbActivo);
            this.panel1.Controls.Add(this.PanelControles);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.btnEditar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbRol);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtUsuario);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 586);
            this.panel1.TabIndex = 0;
            // 
            // rbTrabajadores
            // 
            this.rbTrabajadores.AutoSize = true;
            this.rbTrabajadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbTrabajadores.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbTrabajadores.Location = new System.Drawing.Point(560, 107);
            this.rbTrabajadores.Name = "rbTrabajadores";
            this.rbTrabajadores.Size = new System.Drawing.Size(103, 24);
            this.rbTrabajadores.TabIndex = 31;
            this.rbTrabajadores.TabStop = true;
            this.rbTrabajadores.Text = "Trabajador";
            this.rbTrabajadores.UseVisualStyleBackColor = true;
            this.rbTrabajadores.CheckedChanged += new System.EventHandler(this.rbTrabajadores_CheckedChanged);
            // 
            // rbMensajero
            // 
            this.rbMensajero.AutoSize = true;
            this.rbMensajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbMensajero.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbMensajero.Location = new System.Drawing.Point(453, 107);
            this.rbMensajero.Name = "rbMensajero";
            this.rbMensajero.Size = new System.Drawing.Size(101, 24);
            this.rbMensajero.TabIndex = 30;
            this.rbMensajero.TabStop = true;
            this.rbMensajero.Text = "Mensajero";
            this.rbMensajero.UseVisualStyleBackColor = true;
            this.rbMensajero.CheckedChanged += new System.EventHandler(this.rbMensajero_CheckedChanged);
            // 
            // rbInactivo
            // 
            this.rbInactivo.AutoSize = true;
            this.rbInactivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInactivo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbInactivo.Location = new System.Drawing.Point(272, 326);
            this.rbInactivo.Name = "rbInactivo";
            this.rbInactivo.Size = new System.Drawing.Size(91, 28);
            this.rbInactivo.TabIndex = 29;
            this.rbInactivo.TabStop = true;
            this.rbInactivo.Text = "Inactivo";
            this.rbInactivo.UseVisualStyleBackColor = true;
            this.rbInactivo.Visible = false;
            this.rbInactivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbInactivo_KeyDown);
            // 
            // rbActivo
            // 
            this.rbActivo.AutoSize = true;
            this.rbActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActivo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbActivo.Location = new System.Drawing.Point(184, 326);
            this.rbActivo.Name = "rbActivo";
            this.rbActivo.Size = new System.Drawing.Size(79, 28);
            this.rbActivo.TabIndex = 28;
            this.rbActivo.TabStop = true;
            this.rbActivo.Text = "Activo";
            this.rbActivo.UseVisualStyleBackColor = true;
            this.rbActivo.Visible = false;
            this.rbActivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbActivo_KeyDown);
            // 
            // PanelControles
            // 
            this.PanelControles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.PanelControles.Controls.Add(this.guna2ControlBox3);
            this.PanelControles.Controls.Add(this.guna2ControlBox2);
            this.PanelControles.Controls.Add(this.guna2ControlBox1);
            this.PanelControles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelControles.Location = new System.Drawing.Point(-10, 0);
            this.PanelControles.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(962, 29);
            this.PanelControles.TabIndex = 27;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.BorderRadius = 10;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(889, 0);
            this.guna2ControlBox3.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox3.TabIndex = 2;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.BorderRadius = 10;
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(914, 0);
            this.guna2ControlBox2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox2.TabIndex = 1;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.BorderRadius = 10;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(938, 0);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoEllipsis = true;
            this.btnEliminar.BackColor = System.Drawing.Color.Gold;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.btnEliminar.ForeColor = System.Drawing.Color.Black;
            this.btnEliminar.Location = new System.Drawing.Point(141, 457);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(138, 41);
            this.btnEliminar.TabIndex = 23;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.AutoEllipsis = true;
            this.btnEditar.BackColor = System.Drawing.Color.Gold;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Location = new System.Drawing.Point(241, 382);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(138, 41);
            this.btnEditar.TabIndex = 22;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoEllipsis = true;
            this.btnGuardar.BackColor = System.Drawing.Color.Gold;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(52, 382);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(138, 41);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(63, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 31);
            this.label4.TabIndex = 13;
            this.label4.Text = "Estado:";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(65, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 31);
            this.label3.TabIndex = 12;
            this.label3.Text = "Rol:";
            // 
            // cbRol
            // 
            this.cbRol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbRol.FormattingEnabled = true;
            this.cbRol.Location = new System.Drawing.Point(190, 270);
            this.cbRol.Name = "cbRol";
            this.cbRol.Size = new System.Drawing.Size(139, 28);
            this.cbRol.TabIndex = 11;
            this.cbRol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbRol_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvTrabajadores);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(453, 137);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(448, 404);
            this.panel3.TabIndex = 10;
            // 
            // dgvTrabajadores
            // 
            this.dgvTrabajadores.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTrabajadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrabajadores.Location = new System.Drawing.Point(0, 3);
            this.dgvTrabajadores.Name = "dgvTrabajadores";
            this.dgvTrabajadores.RowHeadersWidth = 51;
            this.dgvTrabajadores.Size = new System.Drawing.Size(448, 401);
            this.dgvTrabajadores.TabIndex = 0;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtUsuario.Location = new System.Drawing.Point(190, 165);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(139, 26);
            this.txtUsuario.TabIndex = 6;
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(63, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNombre.Location = new System.Drawing.Point(190, 215);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(139, 26);
            this.txtNombre.TabIndex = 4;
            this.txtNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombre_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(63, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(3, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(947, 43);
            this.panel2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.label5.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(178)))), ((int)(((byte)(231)))));
            this.label5.Location = new System.Drawing.Point(345, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 38);
            this.label5.TabIndex = 11;
            this.label5.Text = "TRABAJADORES";
            // 
            // AccionMovimiento
            // 
            this.AccionMovimiento.DockIndicatorTransparencyValue = 0.6D;
            this.AccionMovimiento.TargetControl = this.PanelControles;
            this.AccionMovimiento.UseTransparentDrag = true;
            // 
            // Elipse1
            // 
            this.Elipse1.TargetControl = this.txtUsuario;
            // 
            // Elipse2
            // 
            this.Elipse2.TargetControl = this.txtNombre;
            // 
            // Elipse3
            // 
            this.Elipse3.TargetControl = this.cbRol;
            // 
            // Elipse4
            // 
            this.Elipse4.TargetControl = this.btnGuardar;
            // 
            // Elipse5
            // 
            this.Elipse5.TargetControl = this.btnEditar;
            // 
            // Elipse6
            // 
            this.Elipse6.TargetControl = this.btnEliminar;
            // 
            // FrmTrabajador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 580);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrabajador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trabajador";
            this.Load += new System.EventHandler(this.FrmCreacionCajero_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanelControles.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrabajadores)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel PanelControles;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2DragControl AccionMovimiento;
        private System.Windows.Forms.RadioButton rbInactivo;
        private System.Windows.Forms.RadioButton rbActivo;
        private System.Windows.Forms.RadioButton rbTrabajadores;
        private System.Windows.Forms.RadioButton rbMensajero;
        private System.Windows.Forms.DataGridView dgvTrabajadores;
        private Guna.UI2.WinForms.Guna2Elipse Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse Elipse3;
        private Guna.UI2.WinForms.Guna2Elipse Elipse4;
        private Guna.UI2.WinForms.Guna2Elipse Elipse5;
        private Guna.UI2.WinForms.Guna2Elipse Elipse6;
    }
}