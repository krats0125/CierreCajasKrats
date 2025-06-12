using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.AdminSuperCaja
{
    public partial class FrmMovimientosSuperCajaAdmin : Form
    {
        FrmResumenSuperCajaAdmin supercaja;
        CONEXION Conexion = new CONEXION();
        public FrmMovimientosSuperCajaAdmin(FrmResumenSuperCajaAdmin supercaja)
        {
            InitializeComponent();
            this.supercaja = supercaja; 
        }

        private void FrmMovimientosSuperCajaAdmin_Load(object sender, EventArgs e)
        {
            listarConceptos();
            ListarMediosPago();
            ListaMovimientos();
        }
        public void ListaMovimientos()
        {
            string consulta = $@"SELECT 
                                MC.IdMovimiento, 
                                CM.Concepto AS CONCEPTO, 
                                MC.Descripcion AS DESCRIPCION, 
                                 FORMAT(MC.Valor, 'N0', 'es-CO') AS VALOR, 
                                MP.Descripcion AS 'MEDIO DE PAGO', 
                                MC.fecha AS FECHA
                            FROM 
                                MovimientoSuperCaja MC 
                            INNER JOIN 
                                ConceptoMovimiento CM ON MC.IdConcepto = CM.Id 
                            INNER JOIN 
                                MediosDePago MP ON MC.IdMedioPago = MP.IdMedioPago
                            WHERE 
                                MC.IdUsuario = '{supercaja.IdUsuario}' 
                                AND MC.IdCierre = {supercaja.IdCierre}
                            ORDER BY 
                                CM.Concepto
            ";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;
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
            if(cbConceptos.Text== "Datafonos")
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe de agregar el nombre del mensajero al que esta liquidando.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            oMovimiento.IdUsuario = supercaja.IdUsuario;
            oMovimiento.IdCierre = supercaja.IdCierre;
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

                FrmResumenSuperCajaAdmin frmSc = new InstanciasRepository().InstanciaFrmSuperCajaAdmin();
                frmSc.CargarSumatorias();
                frmSc.CitarPanelesMovimientos();
                bool actualizacionExitosa = new SuperCajaAdminRepository(supercaja).ActualizarCierre(supercaja.IdCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                frmSc.CargarCierreVentas();
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
                oMovimiento.IdUsuario = supercaja.IdUsuario;
                oMovimiento.IdCierre = supercaja.IdCierre;
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
                    FrmResumenSuperCajaAdmin frmSc = new InstanciasRepository().InstanciaFrmSuperCajaAdmin();

                    ListaMovimientos();//Muestra en el dgv los movimientos
                    frmSc.CargarSumatorias();//Carga los movimientos al panel de medios de pago(cierre de caja)
                    frmSc.CitarPanelesMovimientos();

                    bool actualizacionExitosa = new SuperCajaAdminRepository(supercaja).ActualizarCierre(supercaja.IdCierre);
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
                        FrmResumenSuperCajaAdmin frmSc = new InstanciasRepository().InstanciaFrmSuperCajaAdmin();

                        ListaMovimientos();
                        frmSc.CargarSumatorias();
                        frmSc.CitarPanelesMovimientos();

                        bool actualizacionExitosa = new SuperCajaAdminRepository(supercaja).ActualizarCierre(supercaja.IdCierre);
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

        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void cbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConceptos.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cbConceptos.SelectedItem;
                string concepto = drv.Row["Concepto"].ToString();
                if (concepto == "Datafonos")
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Datafono Domicilio");
                }
                else
                {
                    cbMediodepago.SelectedIndex = cbMediodepago.FindStringExact("Efectivo");
                }
            }
        }
    }
}
