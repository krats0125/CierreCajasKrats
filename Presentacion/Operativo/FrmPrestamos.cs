using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string consulta = @"SELECT MENSAJEROS.IdTrabajador, MENSAJEROS.nombre, MENSAJEROS.estado
            FROM MENSAJEROS
            WHERE (((MENSAJEROS.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, Conexion.ConexionDbInterna());
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
            
        }

        private void rbTrabajadores_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT TRABAJADORES.IdTrabajador, TRABAJADORES.nombre, TRABAJADORES.estado
            FROM TRABAJADORES
            WHERE (((TRABAJADORES.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, Conexion.ConexionDbInterna());


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
            DataTable ListaConceptosPrestamo = new SentenciaSqlServer().TraerDatos(sql,Conexion.ConexionCierreCaja());
            cbConceptos.DataSource = ListaConceptosPrestamo;

            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        private void CargarTodosTrabajadores()
        {
            string consulta = "";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta,Conexion.ConexionCierreCaja());
            lbxTrabajadores.DataSource = lista;
            lbxTrabajadores.DisplayMember = "";
            lbxTrabajadores.ValueMember = "";
        }

        private void CargarTodosConceptos()
        {
           
            string consulta = "select Id,Concepto from ConceptoAdelanto where activo=1";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, Conexion.ConexionCierreCaja());
            cbConceptos.DataSource= lista;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember= "Id";
        }

        private void lbTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMov=0;
            Movimiento oMovimiento = new Movimiento();
            
            oMovimiento.IdCaja = ppal.idCaja;
            oMovimiento.IdUsuario = ppal.idUsuario;
            oMovimiento.IdCierre = ppal.idCierre;
            oMovimiento.Descripcion = txtObservaciones.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = 9;
            oMovimiento.IdMedioPago =1;

            idMov = new PrestamosRepository().Insertar(oMovimiento);
            if (idMov>0)
            {

                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();
                bool actualizacionExitosa = new CierreCajaRepository().ActualizarCierre(ppal.idCierre);
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

            Prestamo oPrestamo= new Prestamo();

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
            oPrestamo.Cajero=ppal.idUsuario;
            oPrestamo.IdMovimiento = idMov;


            bool esMensajero = rbMensajero.Checked;

            bool seGuardo = new PrestamosRepository().InsertarEnPrestamos(oPrestamo,esMensajero,!esMensajero);
            if (seGuardo)
            {
                MessageBox.Show("Préstamo guardado exitosamente.");
                listarPrestamos();
            }
            else
            {
                MessageBox.Show("Hubo un error guardando el préstamo.");
            }
        }

        public void listarPrestamos()
        {
            //string consulta= $@"SELECT PRESTAMOS.Fecha, MENSAJEROS.nombre, PRESTAMOS.Valor, PRESTAMOS.Concepto 
            //                   FROM MENSAJEROS INNER JOIN PRESTAMOS ON MENSAJEROS.IdTrabajador = PRESTAMOS.IdTrabajador
            //                   WHERE PRESTAMOS.Cajero='{ppal.idUsuario}';";

            string consulta = $@"SELECT PRESTAMOS_MENSAJEROS2.Fecha, MENSAJEROS.nombre, PRESTAMOS_MENSAJEROS2.Valor, PRESTAMOS_MENSAJEROS2.Concepto, PRESTAMOS_MENSAJEROS2.Observacion
                              FROM MENSAJEROS INNER JOIN PRESTAMOS_MENSAJEROS2 ON MENSAJEROS.IdTrabajador = PRESTAMOS_MENSAJEROS2.IdTrabajador
                              WHERE PRESTAMOS_MENSAJEROS2.Cajero='{ppal.idUsuario}'
                              UNION ALL
                              SELECT PRESTAMOS2.Fecha, TRABAJADORES.nombre, PRESTAMOS2.Valor, PRESTAMOS2.Concepto, PRESTAMOS2.Observacion
                              FROM PRESTAMOS2 INNER JOIN TRABAJADORES ON PRESTAMOS2.IdTrabajador = TRABAJADORES.IdTrabajador
                              WHERE PRESTAMOS2.Cajero='{ppal.idUsuario}';";


            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, Conexion.ConexionDbInterna());
            dgvAdelantos.DataSource = lista;
        }
        
    }
}
