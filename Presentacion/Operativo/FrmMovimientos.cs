    using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CierreDeCajas.Presentacion.Sistema
{
    public partial class FrmMovimientos : Form
    {
        Principal ppal = null;
        CONEXION Conexion = new CONEXION();
        public FrmMovimientos(Principal ppal)
        {
            this.ppal = ppal;

            InitializeComponent();
           

        }

        private void Movimientos_Load(object sender, EventArgs e)
        {
            // se llenan los comboBox
            ListaMovimientos();
            listarConceptos();
            ListarMediosPago();

           
          
            
        }

        public void ListaMovimientos()
        {
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto AS CONCEPTO, MC.Descripcion AS DESCRIPCION, MC.Valor AS VALOR, MP.Descripcion AS 'MEDIO DE PAGO', MC.fecha AS FECHA
            from MovimientoCaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{ppal.idUsuario}' and MC.IdCierre= {ppal.idCierre}
			ORDER BY CM.Concepto";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;


        }

        private void ListarMediosPago()
        {
            DataTable listaMediosPago = new SentenciaSqlServer().TraerDatos("select IdMedioPago,Descripcion from MediosDePago", Conexion.ConexionCierreCaja());
            cbMediodepago.DataSource = listaMediosPago;
            cbMediodepago.DisplayMember = "Descripcion";
            cbMediodepago.ValueMember = "IdMedioPago";

        }

        private void listarConceptos()
        {
            DataTable listaConceptos = new SentenciaSqlServer().TraerDatos("Select Id, Concepto from ConceptoMovimiento where activo=1", Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = listaConceptos;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        

        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Al seleccionar una fila se muestra en los campos para ser editada o elimianda
            DataGridViewRow fila= dgvMovimientos.Rows[e.RowIndex];

            lbIdMovimiento.Text=fila.Cells["IdMovimiento"].Value.ToString();
            string Concepto=fila.Cells["Concepto"].Value.ToString();
            cbConceptos.SelectedIndex=cbConceptos.FindStringExact(Concepto);
            txtValor.Text = fila.Cells["Valor"].Value.ToString();
            txtDescripcion.Text=fila.Cells["Descripcion"].Value.ToString();
            string medioPago = fila.Cells["MEDIO DE PAGO"].Value.ToString();
            cbMediodepago.SelectedIndex=cbMediodepago.FindStringExact(medioPago);
            btnGuarda.Enabled=false;
        }

        private void limpiarcampos()
        {
           
            txtValor.Text = "";
            lbIdMovimiento.Text = "";
            txtDescripcion.Text = "";
            cbMediodepago.SelectedIndex = -1;
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbConceptos.Focus();
        }

        public void Enfoque()
        {
            cbConceptos.Focus();
            cbConceptos.Select();
            
        }

        private void FrmMovimientos_Activated(object sender, EventArgs e)
        {
            //cbConceptos.Focus();
        }

        private void FrmMovimientos_Shown(object sender, EventArgs e)
        {
            this.Activate();
            cbConceptos.Focus();
        }

        private void cbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbConceptos.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                string concepto = drv["Concepto"].ToString();

                if (concepto == "Cobro super caja")
                {
                    lbTipoCobro.Visible = true;
                    cbTipodecobro.Visible = true;
                }
                else
                {
                    lbTipoCobro.Visible = false;
                    cbTipodecobro.Visible = false;
                }
            }

        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdCaja = ppal.idCaja;
            oMovimiento.IdUsuario = ppal.idUsuario;
            oMovimiento.IdCierre = ppal.idCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
            if (seInserto)
            {
                MessageBox.Show("Se ha insertado el movimiento correctamente.");
                limpiarcampos();
                ListaMovimientos();
                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();
                bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                frm.CargarCierreVentas();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error insertando el movimiento.");
            }

        }

        private void btnEdita_Click(object sender, EventArgs e)
        {
            Movimiento oMovimiento = new Movimiento();
            oMovimiento.IdMovimiento = Convert.ToInt32(lbIdMovimiento.Text);
            oMovimiento.IdCaja = ppal.idCaja;
            oMovimiento.IdUsuario = ppal.idUsuario;
            oMovimiento.IdCierre = ppal.idCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seActualizo = new MovimientosRepository().Editar(oMovimiento);
            if (seActualizo)
            {
                MessageBox.Show("Se actualizo el movimiento correctamente");
                limpiarcampos();
                btnGuarda.Enabled = true;
                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();

                ListaMovimientos();//Muestra en el dgv los movimientos
                frm.CargarSumatorias();//Carga los movimientos al panel de medios de pago(cierre de caja)
                frm.CitarPanelesMovimientos();

                bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);//Actualiza el panel del cierre de caja
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                frm.CargarCierreVentas();
            }
            else
            {
                MessageBox.Show("Hubo un error al actualizar el movimiento");
                return;
            }

          
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            Movimiento oMovimiento = new Movimiento();


            if (MessageBox.Show("¿Está seguro de que desea eliminar este movimiento?", "Confirmar eliminación",
            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                oMovimiento.IdMovimiento = Convert.ToInt16(lbIdMovimiento.Text);

                bool seElimino = new MovimientosRepository().Eliminar(oMovimiento);


                if (seElimino)
                {
                    MessageBox.Show("El movimiento se ha elimando correctamente");
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();

                    ListaMovimientos();
                    frm.CargarSumatorias();
                    frm.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);//Actualiza el panel del cierre de caja
                    if (!actualizacionExitosa)
                    {
                        MessageBox.Show("Hubo un error actualizando el cierre de caja");
                    }
                    frm.CargarCierreVentas();
                    limpiarcampos();
                    btnGuarda.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el movimiento. Por favor, inténtelo de nuevo.");
                }
            }

        }

        private void cbConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbMediodepago.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(cbMediodepago, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuarda.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(btnGuarda, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void btnGuarda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Movimiento oMovimiento = new Movimiento();

                oMovimiento.IdCaja = ppal.idCaja;
                oMovimiento.IdUsuario = ppal.idUsuario;
                oMovimiento.IdCierre = ppal.idCierre;
                oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
                oMovimiento.Descripcion = txtDescripcion.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

                bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
                if (seInserto)
                {
                    MessageBox.Show("Se ha insertado el movimiento correctamente.");
                    limpiarcampos();
                    ListaMovimientos();
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                    frm.CargarSumatorias();
                    frm.CitarPanelesMovimientos();
                    bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
                    if (!actualizacionExitosa)
                    {
                        MessageBox.Show("Hubo un error actualizando el cierre de caja");
                    }
                    frm.CargarCierreVentas();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error insertando el movimiento.");
                }

            }
        }
    }
}
