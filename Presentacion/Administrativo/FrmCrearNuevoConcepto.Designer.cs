namespace CierreDeCajas.Presentacion.Administrativo
{
    partial class FrmCrearNuevoConcepto
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbAmbos = new System.Windows.Forms.RadioButton();
            this.rbTrabajador = new System.Windows.Forms.RadioButton();
            this.rbMensajero = new System.Windows.Forms.RadioButton();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.AccionMovimiento = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel4.SuspendLayout();
            this.PanelControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbAmbos);
            this.panel4.Controls.Add(this.rbTrabajador);
            this.panel4.Controls.Add(this.rbMensajero);
            this.panel4.Controls.Add(this.txtDescripcion);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnGuardar);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtConcepto);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(5, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(451, 278);
            this.panel4.TabIndex = 24;
            // 
            // rbAmbos
            // 
            this.rbAmbos.AutoSize = true;
            this.rbAmbos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbAmbos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAmbos.ForeColor = System.Drawing.Color.White;
            this.rbAmbos.Location = new System.Drawing.Point(343, 153);
            this.rbAmbos.Name = "rbAmbos";
            this.rbAmbos.Size = new System.Drawing.Size(77, 24);
            this.rbAmbos.TabIndex = 31;
            this.rbAmbos.TabStop = true;
            this.rbAmbos.Text = "Ambos";
            this.rbAmbos.UseVisualStyleBackColor = true;
            this.rbAmbos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbAmbos_KeyDown);
            // 
            // rbTrabajador
            // 
            this.rbTrabajador.AutoSize = true;
            this.rbTrabajador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTrabajador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrabajador.ForeColor = System.Drawing.Color.White;
            this.rbTrabajador.Location = new System.Drawing.Point(236, 153);
            this.rbTrabajador.Name = "rbTrabajador";
            this.rbTrabajador.Size = new System.Drawing.Size(103, 24);
            this.rbTrabajador.TabIndex = 29;
            this.rbTrabajador.TabStop = true;
            this.rbTrabajador.Text = "Trabajador";
            this.rbTrabajador.UseVisualStyleBackColor = true;
            this.rbTrabajador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbTrabajador_KeyDown);
            // 
            // rbMensajero
            // 
            this.rbMensajero.AutoSize = true;
            this.rbMensajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMensajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMensajero.ForeColor = System.Drawing.Color.White;
            this.rbMensajero.Location = new System.Drawing.Point(129, 153);
            this.rbMensajero.Name = "rbMensajero";
            this.rbMensajero.Size = new System.Drawing.Size(101, 24);
            this.rbMensajero.TabIndex = 30;
            this.rbMensajero.TabStop = true;
            this.rbMensajero.Text = "Mensajero";
            this.rbMensajero.UseVisualStyleBackColor = true;
            this.rbMensajero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbMensajero_KeyDown);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDescripcion.Location = new System.Drawing.Point(129, 84);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(287, 56);
            this.txtDescripcion.TabIndex = 28;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(28, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 21);
            this.label10.TabIndex = 27;
            this.label10.Text = "Descripcion:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoEllipsis = true;
            this.btnGuardar.BackColor = System.Drawing.Color.Gold;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(144, 203);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(137, 38);
            this.btnGuardar.TabIndex = 20;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 49);
            this.label1.TabIndex = 21;
            this.label1.Text = "Nuevo concepto:";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtConcepto.Location = new System.Drawing.Point(129, 34);
            this.txtConcepto.Multiline = true;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(287, 34);
            this.txtConcepto.TabIndex = 22;
            this.txtConcepto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConcepto_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(28, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Permiso:";
            // 
            // PanelControles
            // 
            this.PanelControles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.PanelControles.Controls.Add(this.guna2ControlBox3);
            this.PanelControles.Controls.Add(this.guna2ControlBox2);
            this.PanelControles.Controls.Add(this.guna2ControlBox1);
            this.PanelControles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelControles.Location = new System.Drawing.Point(-6, -1);
            this.PanelControles.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(464, 30);
            this.PanelControles.TabIndex = 25;
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
            this.guna2ControlBox3.Location = new System.Drawing.Point(391, 0);
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
            this.guna2ControlBox2.Location = new System.Drawing.Point(416, 0);
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
            this.guna2ControlBox1.Location = new System.Drawing.Point(440, 0);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // AccionMovimiento
            // 
            this.AccionMovimiento.DockIndicatorTransparencyValue = 0.6D;
            this.AccionMovimiento.TargetControl = this.PanelControles;
            this.AccionMovimiento.UseTransparentDrag = true;
            // 
            // Elipse1
            // 
            this.Elipse1.TargetControl = this.btnGuardar;
            // 
            // Elipse2
            // 
            this.Elipse2.TargetControl = this.txtConcepto;
            // 
            // Elipse3
            // 
            this.Elipse3.TargetControl = this.txtDescripcion;
            // 
            // FrmCrearNuevoConcepto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(457, 309);
            this.ControlBox = false;
            this.Controls.Add(this.PanelControles);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCrearNuevoConcepto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCrearNuevoConcepto";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.PanelControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelControles;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.Windows.Forms.RadioButton rbAmbos;
        private System.Windows.Forms.RadioButton rbTrabajador;
        private System.Windows.Forms.RadioButton rbMensajero;
        private Guna.UI2.WinForms.Guna2DragControl AccionMovimiento;
        private Guna.UI2.WinForms.Guna2Elipse Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse Elipse3;
    }
}