using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CierreDeCajas.Modelo;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmMovimientosSuperCaja : Form
    {
        FrmSuperCaja supercaja;
        CONEXION Conexion=new CONEXION();
        public FrmMovimientosSuperCaja(FrmSuperCaja supercaja)
        {
            InitializeComponent();
            this.supercaja = supercaja;
        }
        private void FrmMovimientosSuperCaja_Load(object sender, EventArgs e)
        {
            listarConceptos();
            ListarMediosPago();
            ListaMovimientos();
        }

        public void ListaMovimientos()
        {
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto AS CONCEPTO, MC.Descripcion AS DESCRIPCION, FORMAT(MC.Valor, 'N0', 'es-CO') AS VALOR, MP.Descripcion AS 'MEDIO DE PAGO', MC.fecha AS FECHA
            from MovimientoSuperCaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{supercaja.idUsuario}' and MC.IdCierre= {supercaja.idCierre}
			ORDER BY CM.Concepto
            ";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();

            if (lista.Columns.Count > 1 ) // si la lista esta vacia que no trate de ocultar la columna que no tiene
            {

                dgvMovimientos.Columns[0].Visible = false;
            }
            
        }

        private void ListarMediosPago()
        {
            DataTable listaMediosPago = new SentenciaSqlServer().TraerDatos("select IdMedioPago,Descripcion from MediosDePago where IdMedioPago not in (2,4,5,6,7,8,9,10,11,12,13)", Conexion.ConexionCierreCaja());
            cbMediodepago.DataSource = listaMediosPago;
            cbMediodepago.DisplayMember = "Descripcion";
            cbMediodepago.ValueMember = "IdMedioPago";
        }

        private void listarConceptos()
        {
            DataTable listaConceptos = new SentenciaSqlServer().TraerDatos("SELECT Id, Concepto FROM ConceptoMovimiento WHERE Id NOT IN (3, 4, 7, 8 , 9, 10, 11,12,13,14,15, 16, 17,18) and Activo=1", Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = listaConceptos;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }



        private void limpiarcampos()
        {

            txtValor.Text = "";
            lbIdMovimiento.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            MovimientoSuperCaja oMovimiento = new MovimientoSuperCaja();
            if (cbMediodepago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un medio de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbConceptos.Text == "Datafonos")
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe de agregar el nombre del mensajero al que esta liquidando.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            oMovimiento.IdUsuario = supercaja.idUsuario;
            oMovimiento.IdCierre = supercaja.idCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seInserto = new MovimientoSuperCajaRepository().Insertar(oMovimiento);
            if (seInserto)
            {
                MessageBox.Show("Se ha insertado el movimiento correctamente.");
                limpiarcampos();
                ListaMovimientos();
                
                FrmResumenSuperCaja frmSc= new InstanciasRepository().InstanciaFrmCierreSuperCaja();
                frmSc.CargarSumatorias();
                frmSc.CitarPanelesMovimientos();
                bool actualizacionExitosa = new CierreSuperCajaRepository(supercaja).ActualizarCierre(supercaja.idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                frmSc.CargarCierreVentas();
              txtValor.Focus(); 
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error insertando el movimiento.");
            }
        }

        private void btnEdita_Click(object sender, EventArgs e)
        {
            try
            {
                MovimientoSuperCaja oMovimiento = new MovimientoSuperCaja();
                oMovimiento.IdMovimiento = Convert.ToInt64(lbIdMovimiento.Text);
                oMovimiento.IdUsuario = supercaja.idUsuario;
                oMovimiento.IdCierre = supercaja.idCierre;
                oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
                oMovimiento.Descripcion = txtDescripcion.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

                bool seActualizo = new MovimientoSuperCajaRepository().Editar(oMovimiento);
                if (seActualizo)
                {
                    MessageBox.Show("Se actualizo el movimiento correctamente");
                    limpiarcampos();
                    btnGuarda.Enabled = true;
                    FrmResumenSuperCaja frmSc = new InstanciasRepository().InstanciaFrmCierreSuperCaja();

                    ListaMovimientos();//Muestra en el dgv los movimientos
                    frmSc.CargarSumatorias();//Carga los movimientos al panel de medios de pago(cierre de caja)
                    frmSc.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new CierreSuperCajaRepository(supercaja).ActualizarCierre(supercaja.idCierre);
                    if (!actualizacionExitosa)
                    {
                        MessageBox.Show("Hubo un error actualizando el cierre de caja");
                    }
                    frmSc.CargarCierreVentas();
                }
                else
                {
                    MessageBox.Show("Hubo un error al actualizar el movimiento");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor seleccione el campo que desea editar");
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            try
            {
                MovimientoSuperCaja oMovimiento = new MovimientoSuperCaja();


                if (MessageBox.Show("¿Está seguro de que desea eliminar este movimiento?", "Confirmar eliminación",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    oMovimiento.IdMovimiento = Convert.ToInt64(lbIdMovimiento.Text);

                    bool seElimino = new MovimientoSuperCajaRepository().Eliminar(oMovimiento);


                    if (seElimino)
                    {
                        MessageBox.Show("El movimiento se ha elimando correctamente");
                        FrmResumenSuperCaja frmSc=new InstanciasRepository().InstanciaFrmCierreSuperCaja();

                        ListaMovimientos();
                        frmSc.CargarSumatorias();
                        frmSc.CitarPanelesMovimientos();

                        bool actualizacionExitosa = new CierreSuperCajaRepository(supercaja).ActualizarCierre(supercaja.idCierre);
                        if (!actualizacionExitosa)
                        {
                            MessageBox.Show("Hubo un error actualizando el cierre de caja");
                        }
                        frmSc.CargarCierreVentas();
                        limpiarcampos();
                        btnGuarda.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el movimiento. Por favor, inténtelo de nuevo.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor seleccione el campo que desea eliminar");
            }

        }

        private void dgvMovimientos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Al seleccionar una fila se muestra en los campos para ser editada o elimianda
            DataGridViewRow fila = dgvMovimientos.Rows[e.RowIndex];

            lbIdMovimiento.Text = fila.Cells["IdMovimiento"].Value.ToString();
            string Concepto = fila.Cells["Concepto"].Value.ToString();
            cbConceptos.SelectedIndex = cbConceptos.FindStringExact(Concepto);
            txtValor.Text = fila.Cells["Valor"].Value.ToString();
            txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
            string medioPago = fila.Cells["MEDIO DE PAGO"].Value.ToString();
            cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact(medioPago);
            btnGuarda.Enabled = false;

        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbConceptos.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                string concepto = drv.Row["Concepto"].ToString();
                if (concepto == "Datafonos")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        btnGuarda.Focus();
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        txtDescripcion.Focus();

                    }
                }
            }
        }

        private void cbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbConceptos.SelectedItem!=null)
            {
                DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                string concepto = drv.Row["Concepto"].ToString();
                if(concepto=="Datafonos")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Datafono Domicilio");
                }
                else
                {
                    cbMediodepago.SelectedIndex=cbMediodepago.FindStringExact("Efectivo");
                }
            }
        }

        private void btnGuarda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
               
            }  

        }
    }
}
