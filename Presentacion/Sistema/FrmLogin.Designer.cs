﻿namespace CierreDeCajas
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.btnEntrar = new Guna.UI2.WinForms.Guna2Button();
            this.lbCaja = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.cbUsuario = new System.Windows.Forms.ComboBox();
            this.cbCaja = new System.Windows.Forms.ComboBox();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.AccionMovimientoFormulario = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.ElipFormulario = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lbContraseña = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.rbSuperCaja = new System.Windows.Forms.RadioButton();
            this.rbAdministrativo = new System.Windows.Forms.RadioButton();
            this.rbCierreCaja = new System.Windows.Forms.RadioButton();
            this.PanelControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.label1.Location = new System.Drawing.Point(30, 173);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.SystemColors.Control;
            this.btnEntrar.BorderRadius = 4;
            this.btnEntrar.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEntrar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEntrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEntrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEntrar.FillColor = System.Drawing.Color.Gold;
            this.btnEntrar.Font = new System.Drawing.Font("Arial", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.Black;
            this.btnEntrar.Location = new System.Drawing.Point(97, 374);
            this.btnEntrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.ShadowDecoration.BorderRadius = 10;
            this.btnEntrar.Size = new System.Drawing.Size(131, 37);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // lbCaja
            // 
            this.lbCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.lbCaja.Location = new System.Drawing.Point(47, 433);
            this.lbCaja.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCaja.Name = "lbCaja";
            this.lbCaja.Size = new System.Drawing.Size(99, 27);
            this.lbCaja.TabIndex = 3;
            this.lbCaja.Text = "Caja";
            this.lbCaja.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCaja.Visible = false;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.btnEntrar;
            // 
            // cbUsuario
            // 
            this.cbUsuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUsuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbUsuario.FormattingEnabled = true;
            this.cbUsuario.Location = new System.Drawing.Point(34, 203);
            this.cbUsuario.Margin = new System.Windows.Forms.Padding(2);
            this.cbUsuario.Name = "cbUsuario";
            this.cbUsuario.Size = new System.Drawing.Size(276, 28);
            this.cbUsuario.TabIndex = 4;
            this.cbUsuario.SelectedIndexChanged += new System.EventHandler(this.cbUsuario_SelectedIndexChanged);
            // 
            // cbCaja
            // 
            this.cbCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCaja.FormattingEnabled = true;
            this.cbCaja.Location = new System.Drawing.Point(135, 433);
            this.cbCaja.Margin = new System.Windows.Forms.Padding(9, 2, 2, 2);
            this.cbCaja.Name = "cbCaja";
            this.cbCaja.Size = new System.Drawing.Size(120, 30);
            this.cbCaja.TabIndex = 5;
            this.cbCaja.Visible = false;
            this.cbCaja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCaja_KeyPress);
            // 
            // PanelControles
            // 
            this.PanelControles.Controls.Add(this.label3);
            this.PanelControles.Controls.Add(this.guna2ControlBox3);
            this.PanelControles.Controls.Add(this.guna2ControlBox1);
            this.PanelControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControles.Location = new System.Drawing.Point(0, 0);
            this.PanelControles.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(341, 35);
            this.PanelControles.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.label3.Location = new System.Drawing.Point(8, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cierre De Caja";
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.BorderRadius = 10;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox3.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.guna2ControlBox3.Location = new System.Drawing.Point(280, 2);
            this.guna2ControlBox3.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.PressedColor = System.Drawing.Color.CadetBlue;
            this.guna2ControlBox3.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox3.TabIndex = 2;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.BorderRadius = 10;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(307, 2);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.PressedColor = System.Drawing.Color.CadetBlue;
            this.guna2ControlBox1.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // AccionMovimientoFormulario
            // 
            this.AccionMovimientoFormulario.DockIndicatorTransparencyValue = 0.6D;
            this.AccionMovimientoFormulario.TargetControl = this.PanelControles;
            this.AccionMovimientoFormulario.UseTransparentDrag = true;
            // 
            // ElipFormulario
            // 
            this.ElipFormulario.BorderRadius = 10;
            this.ElipFormulario.TargetControl = this;
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.Black;
            this.guna2CirclePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CirclePictureBox1.Image")));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(0, 34);
            this.guna2CirclePictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(341, 137);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox1.TabIndex = 0;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // lbContraseña
            // 
            this.lbContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContraseña.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.lbContraseña.Location = new System.Drawing.Point(92, 233);
            this.lbContraseña.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbContraseña.Name = "lbContraseña";
            this.lbContraseña.Size = new System.Drawing.Size(141, 49);
            this.lbContraseña.TabIndex = 7;
            this.lbContraseña.Text = "Contraseña";
            this.lbContraseña.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbContraseña.Visible = false;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtContraseña.Location = new System.Drawing.Point(34, 275);
            this.txtContraseña.Multiline = true;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.Size = new System.Drawing.Size(274, 31);
            this.txtContraseña.TabIndex = 11;
            this.txtContraseña.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContraseña_KeyDown);
            // 
            // rbSuperCaja
            // 
            this.rbSuperCaja.AutoSize = true;
            this.rbSuperCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSuperCaja.Location = new System.Drawing.Point(161, 315);
            this.rbSuperCaja.Name = "rbSuperCaja";
            this.rbSuperCaja.Size = new System.Drawing.Size(152, 24);
            this.rbSuperCaja.TabIndex = 12;
            this.rbSuperCaja.TabStop = true;
            this.rbSuperCaja.Text = "Cierre Super Caja";
            this.rbSuperCaja.UseVisualStyleBackColor = true;
            this.rbSuperCaja.Visible = false;
            this.rbSuperCaja.CheckedChanged += new System.EventHandler(this.rbSuperCaja_CheckedChanged);
            this.rbSuperCaja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbSuperCaja_KeyDown);
            // 
            // rbAdministrativo
            // 
            this.rbAdministrativo.AutoSize = true;
            this.rbAdministrativo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbAdministrativo.Location = new System.Drawing.Point(107, 345);
            this.rbAdministrativo.Name = "rbAdministrativo";
            this.rbAdministrativo.Size = new System.Drawing.Size(126, 24);
            this.rbAdministrativo.TabIndex = 13;
            this.rbAdministrativo.TabStop = true;
            this.rbAdministrativo.Text = "Administrativo";
            this.rbAdministrativo.UseVisualStyleBackColor = true;
            this.rbAdministrativo.Visible = false;
            this.rbAdministrativo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbCierresCajeros_KeyDown);
            // 
            // rbCierreCaja
            // 
            this.rbCierreCaja.AutoSize = true;
            this.rbCierreCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbCierreCaja.Location = new System.Drawing.Point(28, 315);
            this.rbCierreCaja.Name = "rbCierreCaja";
            this.rbCierreCaja.Size = new System.Drawing.Size(127, 24);
            this.rbCierreCaja.TabIndex = 14;
            this.rbCierreCaja.TabStop = true;
            this.rbCierreCaja.Text = "Cierre de Caja";
            this.rbCierreCaja.UseVisualStyleBackColor = true;
            this.rbCierreCaja.CheckedChanged += new System.EventHandler(this.rbCierreCaja_CheckedChanged);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(341, 476);
            this.Controls.Add(this.rbCierreCaja);
            this.Controls.Add(this.rbAdministrativo);
            this.Controls.Add(this.rbSuperCaja);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.lbContraseña);
            this.Controls.Add(this.PanelControles);
            this.Controls.Add(this.cbCaja);
            this.Controls.Add(this.cbUsuario);
            this.Controls.Add(this.lbCaja);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";
            this.Load += new System.EventHandler(this.Login_Load);
            this.PanelControles.ResumeLayout(false);
            this.PanelControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnEntrar;
        private System.Windows.Forms.Label lbCaja;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.ComboBox cbUsuario;
        private System.Windows.Forms.ComboBox cbCaja;
        private System.Windows.Forms.Panel PanelControles;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2DragControl AccionMovimientoFormulario;
        private Guna.UI2.WinForms.Guna2Elipse ElipFormulario;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbContraseña;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.RadioButton rbAdministrativo;
        private System.Windows.Forms.RadioButton rbSuperCaja;
        private System.Windows.Forms.RadioButton rbCierreCaja;
    }
}