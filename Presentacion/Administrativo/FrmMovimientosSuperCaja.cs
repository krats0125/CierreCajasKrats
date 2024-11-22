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
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto AS CONCEPTO, MC.Descripcion AS DESCRIPCION, MC.Valor AS VALOR, MP.Descripcion AS 'MEDIO DE PAGO', MC.fecha AS FECHA
            from MovimientoCaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{supercaja.idUsuario}' and MC.IdCierre= {supercaja.idCierre}
			ORDER BY CM.Concepto
            ";

            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            dgvMovimientos.DataSource = lista;
            dgvMovimientos.Refresh();
            dgvMovimientos.Columns[0].Visible = false;
        }

        private void ListarMediosPago()
        {
            DataTable listaMediosPago = new SentenciaSqlServer().TraerDatos("select IdMedioPago,Descripcion from MediosDePago where IdMedioPago not in (2,4,5,6,7,8,9,10,11,12)", Conexion.ConexionCierreCaja());
            cbMediodepago.DataSource = listaMediosPago;
            cbMediodepago.DisplayMember = "Descripcion";
            cbMediodepago.ValueMember = "IdMedioPago";
        }

        private void listarConceptos()
        {
            DataTable listaConceptos = new SentenciaSqlServer().TraerDatos("SELECT Id, Concepto FROM ConceptoMovimiento WHERE Id NOT IN (3, 4, 7, 8, 10, 11,13,14,15, 16, 17) and Activo=1", Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = listaConceptos;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

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

        private void limpiarcampos()
        {

            txtValor.Text = "";
            lbIdMovimiento.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdUsuario = supercaja.idUsuario;
            oMovimiento.IdCierre = supercaja.idCierre;
            oMovimiento.IdMedioPago = Convert.ToInt32(cbMediodepago.SelectedValue.ToString());
            oMovimiento.Descripcion = txtDescripcion.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = Convert.ToInt32(cbConceptos.SelectedValue.ToString());

            bool seInserto = new MovimientosRepository().Insertar(oMovimiento);
            if (seInserto)
            {
                MessageBox.Show("Se ha insertado el movimiento correctamente.");
                limpiarcampos();
                
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
                Movimiento oMovimiento = new Movimiento();
                oMovimiento.IdMovimiento = Convert.ToInt32(lbIdMovimiento.Text);
                oMovimiento.IdUsuario = supercaja.idUsuario;
                oMovimiento.IdCierre = supercaja.idCierre;
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
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor seleccione el campo que desea eliminar");
            }

        }
    }
}
