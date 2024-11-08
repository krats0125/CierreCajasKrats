namespace CierreDeCajas.Presentacion.Administrativo
{
    partial class FrmReportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportes));
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.AccionMovimiento = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinalizarCierre = new System.Windows.Forms.Button();
            this.btnGenerarInforme = new System.Windows.Forms.Button();
            this.pbGenerarInforme = new System.Windows.Forms.PictureBox();
            this.pbFinalizarCierre = new System.Windows.Forms.PictureBox();
            this.Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.PanelControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGenerarInforme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFinalizarCierre)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReporte
            // 
            this.dgvReporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvReporte.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.dgvReporte.Location = new System.Drawing.Point(12, 111);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.Size = new System.Drawing.Size(926, 449);
            this.dgvReporte.TabIndex = 0;
            this.dgvReporte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReporte_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Filtrar por nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNombre.Location = new System.Drawing.Point(27, 71);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(150, 26);
            this.txtNombre.TabIndex = 7;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(775, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Filtrar por fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Location = new System.Drawing.Point(775, 71);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(152, 26);
            this.dtpFecha.TabIndex = 9;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // PanelControles
            // 
            this.PanelControles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.PanelControles.Controls.Add(this.guna2ControlBox3);
            this.PanelControles.Controls.Add(this.guna2ControlBox2);
            this.PanelControles.Controls.Add(this.guna2ControlBox1);
            this.PanelControles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelControles.Location = new System.Drawing.Point(-1, 0);
            this.PanelControles.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(954, 33);
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
            this.guna2ControlBox3.Location = new System.Drawing.Point(881, 0);
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
            this.guna2ControlBox2.Location = new System.Drawing.Point(906, 0);
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
            this.guna2ControlBox1.Location = new System.Drawing.Point(930, 0);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 258);
            this.panel1.TabIndex = 28;
            // 
            // btnFinalizarCierre
            // 
            this.btnFinalizarCierre.AutoEllipsis = true;
            this.btnFinalizarCierre.BackColor = System.Drawing.Color.Gold;
            this.btnFinalizarCierre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizarCierre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarCierre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizarCierre.ForeColor = System.Drawing.Color.Black;
            this.btnFinalizarCierre.Location = new System.Drawing.Point(784, 569);
            this.btnFinalizarCierre.Name = "btnFinalizarCierre";
            this.btnFinalizarCierre.Size = new System.Drawing.Size(154, 34);
            this.btnFinalizarCierre.TabIndex = 29;
            this.btnFinalizarCierre.Text = "Finalizar cierre";
            this.btnFinalizarCierre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFinalizarCierre.UseVisualStyleBackColor = false;
            this.btnFinalizarCierre.Click += new System.EventHandler(this.btnFinalizarCierre_Click);
            // 
            // btnGenerarInforme
            // 
            this.btnGenerarInforme.AutoEllipsis = true;
            this.btnGenerarInforme.BackColor = System.Drawing.Color.Gold;
            this.btnGenerarInforme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarInforme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarInforme.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarInforme.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarInforme.Location = new System.Drawing.Point(27, 569);
            this.btnGenerarInforme.Name = "btnGenerarInforme";
            this.btnGenerarInforme.Size = new System.Drawing.Size(167, 34);
            this.btnGenerarInforme.TabIndex = 30;
            this.btnGenerarInforme.Text = "Generar informes";
            this.btnGenerarInforme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarInforme.UseVisualStyleBackColor = false;
            this.btnGenerarInforme.Click += new System.EventHandler(this.btnGenerarInforme_Click);
            // 
            // pbGenerarInforme
            // 
            this.pbGenerarInforme.BackColor = System.Drawing.Color.Gold;
            this.pbGenerarInforme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbGenerarInforme.Image = ((System.Drawing.Image)(resources.GetObject("pbGenerarInforme.Image")));
            this.pbGenerarInforme.Location = new System.Drawing.Point(30, 571);
            this.pbGenerarInforme.Name = "pbGenerarInforme";
            this.pbGenerarInforme.Size = new System.Drawing.Size(32, 29);
            this.pbGenerarInforme.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGenerarInforme.TabIndex = 32;
            this.pbGenerarInforme.TabStop = false;
            this.pbGenerarInforme.Click += new System.EventHandler(this.pbGenerarInforme_Click);
            // 
            // pbFinalizarCierre
            // 
            this.pbFinalizarCierre.BackColor = System.Drawing.Color.Gold;
            this.pbFinalizarCierre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbFinalizarCierre.Image = ((System.Drawing.Image)(resources.GetObject("pbFinalizarCierre.Image")));
            this.pbFinalizarCierre.Location = new System.Drawing.Point(788, 571);
            this.pbFinalizarCierre.Name = "pbFinalizarCierre";
            this.pbFinalizarCierre.Size = new System.Drawing.Size(33, 29);
            this.pbFinalizarCierre.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFinalizarCierre.TabIndex = 33;
            this.pbFinalizarCierre.TabStop = false;
            this.pbFinalizarCierre.Click += new System.EventHandler(this.pbFinalizarCierre_Click);
            // 
            // Elipse1
            // 
            this.Elipse1.TargetControl = this.btnFinalizarCierre;
            // 
            // Elipse2
            // 
            this.Elipse2.TargetControl = this.btnGenerarInforme;
            // 
            // Elipse3
            // 
            this.Elipse3.TargetControl = this.txtNombre;
            // 
            // Elipse4
            // 
            this.Elipse4.TargetControl = this.dtpFecha;
            // 
            // Elipse5
            // 
            this.Elipse5.TargetControl = this.dgvReporte;
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(950, 612);
            this.Controls.Add(this.pbFinalizarCierre);
            this.Controls.Add(this.pbGenerarInforme);
            this.Controls.Add(this.btnGenerarInforme);
            this.Controls.Add(this.btnFinalizarCierre);
            this.Controls.Add(this.PanelControles);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReportes";
            this.Load += new System.EventHandler(this.FrmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.PanelControles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGenerarInforme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFinalizarCierre)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Panel PanelControles;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2DragControl AccionMovimiento;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFinalizarCierre;
        private System.Windows.Forms.Button btnGenerarInforme;
        private System.Windows.Forms.PictureBox pbGenerarInforme;
        private System.Windows.Forms.PictureBox pbFinalizarCierre;
        private Guna.UI2.WinForms.Guna2Elipse Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse Elipse3;
        private Guna.UI2.WinForms.Guna2Elipse Elipse4;
        private Guna.UI2.WinForms.Guna2Elipse Elipse5;
    }
}