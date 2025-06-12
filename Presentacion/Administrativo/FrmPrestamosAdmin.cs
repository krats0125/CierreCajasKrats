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
    public partial class FrmPrestamosAdmin : Form
    {
        CONEXION Conexion = new CONEXION();
        FrmDetalleReporte FrmDetalle;
        DateTime fechaApertura;
        public FrmPrestamosAdmin(FrmDetalleReporte frmDetalle,DateTime fechaApertura)
        {
            InitializeComponent();
            FrmDetalle = frmDetalle;
            this.fechaApertura = fechaApertura;
        }

        private void FrmPrestamosAdmin_Load(object sender, EventArgs e)
        {

        }

        private void rbMensajero_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT IdTrabajador, nombre, estado
            FROM MENSAJEROS
            WHERE estado=1
			ORDER BY nombre";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.Conexionlabodegadenacho());
            lbxTrabajadores.DataSource = lista;
            lbxTrabajadores.DisplayMember = "nombre";
            lbxTrabajadores.ValueMember = "IdTrabajador";
            listarConceptos();
        }

        private void rbTrabajadores_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT IdTrabajador, nombre, estado
            FROM TRABAJADORES
            WHERE estado=1
			ORDER BY nombre";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.Conexionlabodegadenacho());

            lbxTrabajadores.DataSource = lista;
            lbxTrabajadores.DisplayMember = "nombre";
            lbxTrabajadores.ValueMember = "IdTrabajador";
            listarConceptos();
        }
        private void listarConceptos()
        {

            string sql = "";
            if (rbMensajero.Checked)
            {
                sql = "select Id,Concepto from ConceptoAdelanto where Activo=1 and ( Permiso=2 or Permiso=1)";

            }
            else if (rbTrabajadores.Checked)
            {
                sql = "select Id,Concepto from ConceptoAdelanto where activo = 1 and ( Permiso=3 or Permiso=1)";
            }
            else
            {
                sql = "select Id,Concepto from ConceptoAdelanto where activo=1 and permiso=1";
            }
            DataTable ListaConceptosPrestamo = new SentenciaSqlServer().TraerDatos(sql, Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = ListaConceptosPrestamo;

            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMov = 0;
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdCaja = FrmDetalle.idcaja;
            oMovimiento.IdUsuario = FrmDetalle.IdUsuario;
            oMovimiento.IdCierre = FrmDetalle.IdCierre;
            oMovimiento.Descripcion = txtObservaciones.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = 9;
            oMovimiento.IdMedioPago = 1;

            idMov = new PrestamosRepository().Insertar(oMovimiento);
            if (idMov > 0)
            {

                FrmDetalleReporte frm = new InstanciasRepository().InstanciaFrmDetalle();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();
                bool actualizacionExitosa = new DetalleReporteRepository(FrmDetalle,fechaApertura).ActualizarCierre(FrmDetalle.IdCierre);
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

            Prestamo oPrestamo = new Prestamo();

            if (rbMensajero.Checked)
            {
                oPrestamo.IdMensajero = lbxTrabajadores.SelectedValue.ToString();
            }
            else if (rbTrabajadores.Checked)
            {
                oPrestamo.IdTrabajador = lbxTrabajadores.SelectedValue.ToString();
            }

            oPrestamo.Valor = Convert.ToDecimal(txtValor.Text);
            oPrestamo.Concepto = cbConceptos.Text;
            oPrestamo.Observacion = txtObservaciones.Text;
            oPrestamo.Caja = Convert.ToInt64(FrmDetalle.idcaja).ToString();
            oPrestamo.Cajero = FrmDetalle.IdUsuario;
            oPrestamo.IdMovimiento = idMov;


            bool esMensajero = rbMensajero.Checked;

            bool seGuardo = new PrestamosRepository().InsertarEnPrestamos(oPrestamo, esMensajero, !esMensajero);
            if (seGuardo)
            {
                MessageBox.Show("Préstamo guardado exitosamente.");
                txtValor.Clear();
                txtObservaciones.Clear();
                listarPrestamos();
            }
            else
            {
                MessageBox.Show("Hubo un error guardando el préstamo.");
            }

        }

        public void listarPrestamos()
        {

            string consulta = $@"SELECT 
                                pm.Fecha AS FECHA, 
                                m.nombre AS NOMBRE, 
                                FORMAT(pm.Valor, 'C0', 'es-CO') AS VALOR, 
                                pm.Concepto AS CONCEPTO, 
                                pm.Observacion AS OBSERVACIONES
                            FROM 
                                MENSAJEROS m 
                            INNER JOIN 
                                PRESTAMOS_MENSAJEROS pm ON m.IdTrabajador = pm.IdTrabajador
                            WHERE 
                                pm.Cajero = '{FrmDetalle.IdUsuario}' 
                                AND pm.Pagado = 0 
                                AND CONVERT(DATE, pm.Fecha) = CONVERT(date, DATEADD(HOUR, -5, GETDATE()))
                            
                            UNION ALL
                            
                            SELECT 
                                P.Fecha AS FECHA, 
                                T.nombre AS NOMBRE, 
                                FORMAT(P.Valor, 'C0', 'es-CO') AS VALOR, 
                                P.Concepto AS CONCEPTO, 
                                P.Observacion AS OBSERVACION
                            FROM 
                                PRESTAMOS P 
                            INNER JOIN 
                                TRABAJADORES T ON P.IdTrabajador = T.IdTrabajador
                            WHERE 
                                P.Cajero = '{FrmDetalle.IdUsuario}'  
                                AND P.Pagado = 0 
                                AND CONVERT(DATE, P.Fecha) = CONVERT(date, DATEADD(HOUR, -5, GETDATE()))";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.Conexionlabodegadenacho());
            dgvAdelantos.DataSource = lista;
        }
    }
}
