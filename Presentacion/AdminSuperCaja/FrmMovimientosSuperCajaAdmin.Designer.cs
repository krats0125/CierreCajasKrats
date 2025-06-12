namespace CierreDeCajas.Presentacion.AdminSuperCaja
{
    partial class FrmMovimientosSuperCajaAdmin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMovimientosSuperCajaAdmin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbCajero = new System.Windows.Forms.Label();
            this.btnElimina = new System.Windows.Forms.Button();
            this.btnEdita = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.btnGuarda = new System.Windows.Forms.Button();
            this.cbTipodecobro = new System.Windows.Forms.ComboBox();
            this.cbMediodepago = new System.Windows.Forms.ComboBox();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTipoCobro = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbIdMovimiento = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.AccionMovimiento = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.PanelControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.PanelControles);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 528);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbCajero);
            this.panel3.Controls.Add(this.btnElimina);
            this.panel3.Controls.Add(this.btnEdita);
            this.panel3.Controls.Add(this.txtDescripcion);
            this.panel3.Controls.Add(this.txtValor);
            this.panel3.Controls.Add(this.btnGuarda);
            this.panel3.Controls.Add(this.cbTipodecobro);
            this.panel3.Controls.Add(this.cbMediodepago);
            this.panel3.Controls.Add(this.cbConceptos);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lbTipoCobro);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.lbIdMovimiento);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(2, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(789, 501);
            this.panel3.TabIndex = 42;
            // 
            // lbCajero
            // 
            this.lbCajero.AutoSize = true;
            this.lbCajero.Location = new System.Drawing.Point(50, 4);
            this.lbCajero.Name = "lbCajero";
            this.lbCajero.Size = new System.Drawing.Size(35, 13);
            this.lbCajero.TabIndex = 40;
            this.lbCajero.Text = "label6";
            this.lbCajero.Visible = false;
            // 
            // btnElimina
            // 
            this.btnElimina.AutoEllipsis = true;
            this.btnElimina.BackColor = System.Drawing.Color.Gold;
            this.btnElimina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnElimina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElimina.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElimina.ForeColor = System.Drawing.Color.Black;
            this.btnElimina.Location = new System.Drawing.Point(631, 196);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(85, 35);
            this.btnElimina.TabIndex = 38;
            this.btnElimina.Text = "Eliminar";
            this.btnElimina.UseVisualStyleBackColor = false;
            this.btnElimina.Click += new System.EventHandler(this.btnElimina_Click);
            // 
            // btnEdita
            // 
            this.btnEdita.AutoEllipsis = true;
            this.btnEdita.BackColor = System.Drawing.Color.Gold;
            this.btnEdita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdita.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdita.ForeColor = System.Drawing.Color.Black;
            this.btnEdita.Location = new System.Drawing.Point(519, 196);
            this.btnEdita.Name = "btnEdita";
            this.btnEdita.Size = new System.Drawing.Size(85, 35);
            this.btnEdita.TabIndex = 37;
            this.btnEdita.Text = "Editar";
            this.btnEdita.UseVisualStyleBackColor = false;
            this.btnEdita.Click += new System.EventHandler(this.btnEdita_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDescripcion.Location = new System.Drawing.Point(498, 100);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 82);
            this.txtDescripcion.TabIndex = 36;
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValor.Location = new System.Drawing.Point(131, 101);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(200, 26);
            this.txtValor.TabIndex = 35;
            // 
            // btnGuarda
            // 
            this.btnGuarda.AutoEllipsis = true;
            this.btnGuarda.BackColor = System.Drawing.Color.Gold;
            this.btnGuarda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuarda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuarda.ForeColor = System.Drawing.Color.Black;
            this.btnGuarda.Location = new System.Drawing.Point(407, 196);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(85, 35);
            this.btnGuarda.TabIndex = 33;
            this.btnGuarda.Text = "Guardar";
            this.btnGuarda.UseVisualStyleBackColor = false;
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // cbTipodecobro
            // 
            this.cbTipodecobro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTipodecobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbTipodecobro.FormattingEnabled = true;
            this.cbTipodecobro.Items.AddRange(new object[] {
            "Efectivo pendiente",
            "Datafono pendiente",
            "Efectivo cobrado",
            "Datafono cobrado"});
            this.cbTipodecobro.Location = new System.Drawing.Point(132, 167);
            this.cbTipodecobro.Name = "cbTipodecobro";
            this.cbTipodecobro.Size = new System.Drawing.Size(200, 28);
            this.cbTipodecobro.TabIndex = 32;
            this.cbTipodecobro.Visible = false;
            // 
            // cbMediodepago
            // 
            this.cbMediodepago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMediodepago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbMediodepago.FormattingEnabled = true;
            this.cbMediodepago.Location = new System.Drawing.Point(498, 36);
            this.cbMediodepago.Name = "cbMediodepago";
            this.cbMediodepago.Size = new System.Drawing.Size(200, 28);
            this.cbMediodepago.TabIndex = 31;
            // 
            // cbConceptos
            // 
            this.cbConceptos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbConceptos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(131, 36);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(200, 28);
            this.cbConceptos.TabIndex = 30;
            this.cbConceptos.SelectedIndexChanged += new System.EventHandler(this.cbConceptos_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(378, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "Descripción:";
            // 
            // lbTipoCobro
            // 
            this.lbTipoCobro.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoCobro.ForeColor = System.Drawing.Color.White;
            this.lbTipoCobro.Location = new System.Drawing.Point(22, 150);
            this.lbTipoCobro.Name = "lbTipoCobro";
            this.lbTipoCobro.Size = new System.Drawing.Size(81, 55);
            this.lbTipoCobro.TabIndex = 25;
            this.lbTipoCobro.Text = "Tipo de cobro:";
            this.lbTipoCobro.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(355, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 35);
            this.label4.TabIndex = 23;
            this.label4.Text = "Medio de pago:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(16, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "Valor:";
            // 
            // lbIdMovimiento
            // 
            this.lbIdMovimiento.AutoSize = true;
            this.lbIdMovimiento.Location = new System.Drawing.Point(9, 4);
            this.lbIdMovimiento.Name = "lbIdMovimiento";
            this.lbIdMovimiento.Size = new System.Drawing.Size(35, 13);
            this.lbIdMovimiento.TabIndex = 19;
            this.lbIdMovimiento.Text = "label6";
            this.lbIdMovimiento.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "Concepto:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvMovimientos);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(15, 259);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(748, 201);
            this.panel2.TabIndex = 41;
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMovimientos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.dgvMovimientos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMovimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMovimientos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMovimientos.EnableHeadersVisualStyles = false;
            this.dgvMovimientos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.dgvMovimientos.Location = new System.Drawing.Point(0, 3);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMovimientos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMovimientos.Size = new System.Drawing.Size(748, 195);
            this.dgvMovimientos.TabIndex = 39;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // PanelControles
            // 
            this.PanelControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.PanelControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelControles.Controls.Add(this.guna2ControlBox3);
            this.PanelControles.Controls.Add(this.guna2ControlBox1);
            this.PanelControles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControles.Location = new System.Drawing.Point(0, 0);
            this.PanelControles.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(792, 26);
            this.PanelControles.TabIndex = 34;
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
            this.guna2ControlBox3.Location = new System.Drawing.Point(740, 0);
            this.guna2ControlBox3.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(22, 24);
            this.guna2ControlBox3.TabIndex = 2;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.BorderRadius = 10;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(766, 0);
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
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.btnGuarda;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.TargetControl = this.btnEdita;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.TargetControl = this.btnElimina;
            // 
            // FrmMovimientosSuperCajaAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(791, 528);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMovimientosSuperCajaAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos Super Caja Administrativo";
            this.Load += new System.EventHandler(this.FrmMovimientosSuperCajaAdmin_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.PanelControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbCajero;
        private System.Windows.Forms.Button btnElimina;
        private System.Windows.Forms.Button btnEdita;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Panel PanelControles;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.Windows.Forms.Button btnGuarda;
        private System.Windows.Forms.ComboBox cbTipodecobro;
        private System.Windows.Forms.ComboBox cbMediodepago;
        private System.Windows.Forms.ComboBox cbConceptos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTipoCobro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbIdMovimiento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private Guna.UI2.WinForms.Guna2DragControl AccionMovimiento;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
    }
}