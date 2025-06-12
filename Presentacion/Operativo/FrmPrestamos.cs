using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion
{
    public partial class FrmPrestamos : Form
    {
        CONEXION Conexion = new CONEXION();

        Principal ppal;
        FrmMovimientos mov;
        private decimal valorEntregado = 0;

        public FrmPrestamos(Principal ppal)
        {
            InitializeComponent();
            this.ppal = ppal;

        }

        private void rbMensajero_CheckedChanged(object sender, EventArgs e)
        {
            //string consulta = @"SELECT MENSAJEROS.IdTrabajador, MENSAJEROS.nombre, MENSAJEROS.estado
            //FROM MENSAJEROS
            //WHERE (((MENSAJEROS.estado)=True));";
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



        private void FrmPrestamos_Load(object sender, EventArgs e)
        {
            //CargarTodosTrabajadores();
            CargarTodosConceptos();
            listarPrestamos();
            dgvAdelantos.ClearSelection();
        }

        private void rbTrabajadores_CheckedChanged(object sender, EventArgs e)
        {
            //string consulta = @"SELECT TRABAJADORES.IdTrabajador, TRABAJADORES.nombre, TRABAJADORES.estado
            //FROM TRABAJADORES
            //WHERE (((TRABAJADORES.estado)=True));";
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

        private void CargarTodosTrabajadores()
        {
            string consulta = "";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            lbxTrabajadores.DataSource = lista;
            lbxTrabajadores.DisplayMember = "";
            lbxTrabajadores.ValueMember = "";
        }

        private void CargarTodosConceptos()
        {

            string consulta = "select Id,Concepto from ConceptoAdelanto where activo=1";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = lista;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";
        }

        private void lbTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMov = 0;
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdCaja = ppal.idCaja;
            oMovimiento.IdUsuario = ppal.idUsuario;
            oMovimiento.IdCierre = ppal.idCierre;
            oMovimiento.Descripcion = txtObservaciones.Text;
            oMovimiento.Valor = Convert.ToDecimal( txtValor.Text);
            oMovimiento.IdConcepto = 9;
            oMovimiento.IdMedioPago = 1;

            idMov = new PrestamosRepository().Insertar(oMovimiento);
            if (idMov > 0)
            {

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
            oPrestamo.Caja = Convert.ToInt64(ppal.idCaja).ToString();
            oPrestamo.Cajero = ppal.idUsuario;
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
                                pm.Cajero = '{ppal.idUsuario}' 
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
                                P.Cajero = '{ppal.idUsuario}'  
                                AND P.Pagado = 0 
                                AND CONVERT(DATE, P.Fecha) = CONVERT(date, DATEADD(HOUR, -5, GETDATE()))";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.Conexionlabodegadenacho());
            dgvAdelantos.DataSource = lista;
        }

        private void rbMensajero_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbConceptos.Focus();
            }
             else if (e.KeyCode == Keys.Right)
            {
                cbConceptos.Focus();
                e.SuppressKeyPress = true;
            }
            
        }

        private void cbConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtObservaciones.Focus();
            }
        }

        private void txtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuardar.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(btnGuardar, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
        }

        private void btnGuardar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int idMov = 0;
                Movimiento oMovimiento = new Movimiento();

                oMovimiento.IdCaja = ppal.idCaja;
                oMovimiento.IdUsuario = ppal.idUsuario;
                oMovimiento.IdCierre = ppal.idCierre;
                oMovimiento.Descripcion = txtObservaciones.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = 9;
                oMovimiento.IdMedioPago = 1;

                idMov = new PrestamosRepository().Insertar(oMovimiento);
                if (idMov > 0)
                {

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
                oPrestamo.Caja = Convert.ToInt64(ppal.idCaja).ToString();
                oPrestamo.Cajero = ppal.idUsuario;
                oPrestamo.IdMovimiento = idMov;


                bool esMensajero = rbMensajero.Checked;

                bool seGuardo = new PrestamosRepository().InsertarEnPrestamos(oPrestamo, esMensajero, !esMensajero);
                if (seGuardo)
                {
                    MessageBox.Show("Préstamo guardado exitosamente.");
                    listarPrestamos();
                    txtValor.Clear();
                    txtObservaciones.Clear();
                }
                else
                {
                    MessageBox.Show("Hubo un error guardando el préstamo.");
                }
            }
        }

        private void rbMensajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                // Mover el foco al control siguiente
                this.SelectNextControl(lbxTrabajadores, true, true, true, true);
                e.SuppressKeyPress = true; // Evitar la acción predeterminada
            }
            else if (e.KeyCode == Keys.Right)
            {
                rbTrabajadores.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void lbxTrabajadores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
            }
        }
    }
}
