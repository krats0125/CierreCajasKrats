namespace CierreDeCajas.Presentacion.Sistema
{
    partial class FrmMovimientos
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
            this.PanelTitulo = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnElimina = new System.Windows.Forms.Button();
            this.btnEdita = new System.Windows.Forms.Button();
            this.btnGuarda = new System.Windows.Forms.Button();
            this.lbTipoCobro = new System.Windows.Forms.Label();
            this.cbTipodecobro = new System.Windows.Forms.ComboBox();
            this.lbIdMovimiento = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.cbMediodepago = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PanelTitulo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PanelTitulo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 561);
            this.panel1.TabIndex = 0;
            // 
            // PanelTitulo
            // 
            this.PanelTitulo.Controls.Add(this.label5);
            this.PanelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelTitulo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PanelTitulo.Name = "PanelTitulo";
            this.PanelTitulo.Size = new System.Drawing.Size(1036, 42);
            this.PanelTitulo.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(178)))), ((int)(((byte)(231)))));
            this.label5.Location = new System.Drawing.Point(328, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(382, 38);
            this.label5.TabIndex = 11;
            this.label5.Text = "MOVIMIENTOS DE CAJA ";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnElimina);
            this.panel3.Controls.Add(this.btnEdita);
            this.panel3.Controls.Add(this.btnGuarda);
            this.panel3.Controls.Add(this.lbTipoCobro);
            this.panel3.Controls.Add(this.cbTipodecobro);
            this.panel3.Controls.Add(this.lbIdMovimiento);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.txtValor);
            this.panel3.Controls.Add(this.cbMediodepago);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtDescripcion);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cbConceptos);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(101, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(822, 489);
            this.panel3.TabIndex = 1;
            // 
            // btnElimina
            // 
            this.btnElimina.AutoEllipsis = true;
            this.btnElimina.BackColor = System.Drawing.Color.Gold;
            this.btnElimina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnElimina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElimina.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElimina.ForeColor = System.Drawing.Color.Black;
            this.btnElimina.Location = new System.Drawing.Point(676, 191);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(85, 31);
            this.btnElimina.TabIndex = 24;
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
            this.btnEdita.Location = new System.Drawing.Point(574, 191);
            this.btnEdita.Name = "btnEdita";
            this.btnEdita.Size = new System.Drawing.Size(85, 31);
            this.btnEdita.TabIndex = 23;
            this.btnEdita.Text = "Editar";
            this.btnEdita.UseVisualStyleBackColor = false;
            this.btnEdita.Click += new System.EventHandler(this.btnEdita_Click);
            // 
            // btnGuarda
            // 
            this.btnGuarda.AutoEllipsis = true;
            this.btnGuarda.BackColor = System.Drawing.Color.Gold;
            this.btnGuarda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuarda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuarda.ForeColor = System.Drawing.Color.Black;
            this.btnGuarda.Location = new System.Drawing.Point(472, 191);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(85, 31);
            this.btnGuarda.TabIndex = 22;
            this.btnGuarda.Text = "Guardar";
            this.btnGuarda.UseVisualStyleBackColor = false;
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            this.btnGuarda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuarda_KeyDown);
            // 
            // lbTipoCobro
            // 
            this.lbTipoCobro.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoCobro.ForeColor = System.Drawing.Color.White;
            this.lbTipoCobro.Location = new System.Drawing.Point(32, 178);
            this.lbTipoCobro.Name = "lbTipoCobro";
            this.lbTipoCobro.Size = new System.Drawing.Size(132, 60);
            this.lbTipoCobro.TabIndex = 18;
            this.lbTipoCobro.Text = "Tipo de cobro:";
            this.lbTipoCobro.Visible = false;
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
            this.cbTipodecobro.Location = new System.Drawing.Point(162, 205);
            this.cbTipodecobro.Name = "cbTipodecobro";
            this.cbTipodecobro.Size = new System.Drawing.Size(200, 28);
            this.cbTipodecobro.TabIndex = 17;
            this.cbTipodecobro.Visible = false;
            this.cbTipodecobro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTipodecobro_KeyDown);
            // 
            // lbIdMovimiento
            // 
            this.lbIdMovimiento.AutoSize = true;
            this.lbIdMovimiento.Location = new System.Drawing.Point(228, 0);
            this.lbIdMovimiento.Name = "lbIdMovimiento";
            this.lbIdMovimiento.Size = new System.Drawing.Size(35, 13);
            this.lbIdMovimiento.TabIndex = 16;
            this.lbIdMovimiento.Text = "label6";
            this.lbIdMovimiento.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvMovimientos);
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(17, 255);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(792, 213);
            this.panel4.TabIndex = 10;
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimientos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.White;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvMovimientos.Location = new System.Drawing.Point(-17, 0);
            this.dgvMovimientos.MultiSelect = false;
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.RowHeadersWidth = 51;
            this.dgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.Size = new System.Drawing.Size(806, 213);
            this.dgvMovimientos.TabIndex = 0;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValor.Location = new System.Drawing.Point(162, 82);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(200, 26);
            this.txtValor.TabIndex = 1;
            this.txtValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValor_KeyDown);
            // 
            // cbMediodepago
            // 
            this.cbMediodepago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMediodepago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbMediodepago.FormattingEnabled = true;
            this.cbMediodepago.Location = new System.Drawing.Point(162, 141);
            this.cbMediodepago.Name = "cbMediodepago";
            this.cbMediodepago.Size = new System.Drawing.Size(200, 28);
            this.cbMediodepago.TabIndex = 3;
            this.cbMediodepago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbMediodepago_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(32, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Valor:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(32, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 60);
            this.label4.TabIndex = 6;
            this.label4.Text = "Medio de pago:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDescripcion.Location = new System.Drawing.Point(523, 41);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(229, 124);
            this.txtDescripcion.TabIndex = 2;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(381, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descripción:";
            // 
            // cbConceptos
            // 
            this.cbConceptos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbConceptos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(162, 30);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(200, 28);
            this.cbConceptos.TabIndex = 0;
            this.cbConceptos.SelectedIndexChanged += new System.EventHandler(this.cbConceptos_SelectedIndexChanged);
            this.cbConceptos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbConceptos_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Concepto:";
            // 
            // FrmMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1036, 561);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMovimientos";
            this.Text = "Movimientos";
            this.Activated += new System.EventHandler(this.FrmMovimientos_Activated);
            this.Load += new System.EventHandler(this.Movimientos_Load);
            this.Shown += new System.EventHandler(this.FrmMovimientos_Shown);
            this.panel1.ResumeLayout(false);
            this.PanelTitulo.ResumeLayout(false);
            this.PanelTitulo.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbConceptos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMediodepago;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbIdMovimiento;
        private Guna.UI2.WinForms.Guna2Panel PanelTitulo;
        private System.Windows.Forms.ComboBox cbTipodecobro;
        private System.Windows.Forms.Label lbTipoCobro;
        private System.Windows.Forms.Button btnGuarda;
        private System.Windows.Forms.Button btnElimina;
        private System.Windows.Forms.Button btnEdita;
        public System.Windows.Forms.DataGridView dgvMovimientos;
    }
}