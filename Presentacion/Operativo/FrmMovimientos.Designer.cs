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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelTitulo = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvRepetidos = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
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
            this.Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse6 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse7 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse8 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.Elipse9 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel1.SuspendLayout();
            this.PanelTitulo.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepetidos)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.PanelTitulo.Margin = new System.Windows.Forms.Padding(2);
            this.PanelTitulo.Name = "PanelTitulo";
            this.PanelTitulo.Size = new System.Drawing.Size(1036, 42);
            this.PanelTitulo.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(178)))), ((int)(((byte)(231)))));
            this.label5.Location = new System.Drawing.Point(328, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(382, 38);
            this.label5.TabIndex = 11;
            this.label5.Text = "MOVIMIENTOS DE CAJA ";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.dgvRepetidos);
            this.panel3.Controls.Add(this.label6);
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
            this.panel3.Location = new System.Drawing.Point(30, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(994, 501);
            this.panel3.TabIndex = 1;
            // 
            // dgvRepetidos
            // 
            this.dgvRepetidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRepetidos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.dgvRepetidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRepetidos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRepetidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRepetidos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRepetidos.EnableHeadersVisualStyles = false;
            this.dgvRepetidos.Location = new System.Drawing.Point(459, 47);
            this.dgvRepetidos.Name = "dgvRepetidos";
            this.dgvRepetidos.RowHeadersVisible = false;
            this.dgvRepetidos.RowHeadersWidth = 51;
            this.dgvRepetidos.Size = new System.Drawing.Size(522, 55);
            this.dgvRepetidos.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(624, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 29);
            this.label6.TabIndex = 45;
            this.label6.Text = "Movimientos repetidos:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnElimina
            // 
            this.btnElimina.AutoEllipsis = true;
            this.btnElimina.BackColor = System.Drawing.Color.Gold;
            this.btnElimina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnElimina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElimina.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElimina.ForeColor = System.Drawing.Color.Black;
            this.btnElimina.Location = new System.Drawing.Point(172, 445);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(82, 31);
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
            this.btnEdita.Location = new System.Drawing.Point(95, 445);
            this.btnEdita.Name = "btnEdita";
            this.btnEdita.Size = new System.Drawing.Size(68, 31);
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
            this.btnGuarda.Location = new System.Drawing.Point(5, 445);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(83, 31);
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
            this.lbTipoCobro.Location = new System.Drawing.Point(215, 15);
            this.lbTipoCobro.Name = "lbTipoCobro";
            this.lbTipoCobro.Size = new System.Drawing.Size(161, 33);
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
            this.cbTipodecobro.Location = new System.Drawing.Point(220, 52);
            this.cbTipodecobro.Name = "cbTipodecobro";
            this.cbTipodecobro.Size = new System.Drawing.Size(200, 28);
            this.cbTipodecobro.TabIndex = 17;
            this.cbTipodecobro.Visible = false;
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
            this.panel4.Location = new System.Drawing.Point(269, 112);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(706, 367);
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
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            this.dgvMovimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMovimientos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMovimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(48)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMovimientos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMovimientos.EnableHeadersVisualStyles = false;
            this.dgvMovimientos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(23)))), ((int)(((byte)(50)))));
            this.dgvMovimientos.Location = new System.Drawing.Point(3, 3);
            this.dgvMovimientos.MultiSelect = false;
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvMovimientos.RowHeadersVisible = false;
            this.dgvMovimientos.RowHeadersWidth = 51;
            this.dgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.Size = new System.Drawing.Size(706, 355);
            this.dgvMovimientos.TabIndex = 0;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValor.Location = new System.Drawing.Point(8, 123);
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
            this.cbMediodepago.Location = new System.Drawing.Point(8, 203);
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
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Valor:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 37);
            this.label4.TabIndex = 6;
            this.label4.Text = "Medio de pago:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDescripcion.Location = new System.Drawing.Point(9, 285);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(199, 142);
            this.txtDescripcion.TabIndex = 2;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 247);
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
            this.cbConceptos.Location = new System.Drawing.Point(8, 52);
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
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Concepto:";
            // 
            // Elipse1
            // 
            this.Elipse1.TargetControl = this.cbConceptos;
            // 
            // Elipse2
            // 
            this.Elipse2.TargetControl = this.txtValor;
            // 
            // Elipse3
            // 
            this.Elipse3.TargetControl = this.cbMediodepago;
            // 
            // Elipse4
            // 
            this.Elipse4.TargetControl = this.cbTipodecobro;
            // 
            // Elipse5
            // 
            this.Elipse5.TargetControl = this.txtDescripcion;
            // 
            // Elipse6
            // 
            this.Elipse6.TargetControl = this.btnGuarda;
            // 
            // Elipse7
            // 
            this.Elipse7.TargetControl = this.btnElimina;
            // 
            // Elipse8
            // 
            this.Elipse8.TargetControl = this.btnElimina;
            // 
            // Elipse9
            // 
            this.Elipse9.TargetControl = this.dgvMovimientos;
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepetidos)).EndInit();
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
        private Guna.UI2.WinForms.Guna2Elipse Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse Elipse3;
        private Guna.UI2.WinForms.Guna2Elipse Elipse4;
        private Guna.UI2.WinForms.Guna2Elipse Elipse5;
        private Guna.UI2.WinForms.Guna2Elipse Elipse6;
        private Guna.UI2.WinForms.Guna2Elipse Elipse7;
        private Guna.UI2.WinForms.Guna2Elipse Elipse8;
        private Guna.UI2.WinForms.Guna2Elipse Elipse9;
        public System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.DataGridView dgvRepetidos;
        private System.Windows.Forms.Label label6;
    }
}