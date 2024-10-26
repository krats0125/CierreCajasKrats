using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmNuevoAdelanto : Form
    {
        CONEXION conexion = new CONEXION();
       
        FrmAdministrativo admin=null;
        public FrmNuevoAdelanto(FrmAdministrativo admin)
        {
            InitializeComponent();
            this.admin=admin;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMov = 0;
            Movimiento oMovimiento = new Movimiento();

            oMovimiento.IdUsuario = admin.idUsuario;
            oMovimiento.Descripcion = txtObservaciones.Text;
            oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
            oMovimiento.IdConcepto = 9;

            idMov = new PrestamosRepository().Insertar(oMovimiento);


            Prestamo oPrestamo = new Prestamo();

            if (rbMensajero.Checked)
            {
                oPrestamo.IdMensajero = lbxTrabajadores.SelectedValue.ToString();
            }
            else if (rbTrabajador.Checked)
            {
                oPrestamo.IdTrabajador = lbxTrabajadores.SelectedValue.ToString();
            }

            oPrestamo.Valor = Convert.ToDecimal(txtValor.Text);
            oPrestamo.Concepto = cbConceptos.Text;
            oPrestamo.Observacion = txtObservaciones.Text;
            oPrestamo.Cajero = admin.idUsuario;
            oPrestamo.Caja = 0.ToString();
            oPrestamo.IdMovimiento = idMov;


            bool esMensajero = rbMensajero.Checked;

            bool seGuardo = new PrestamosRepository().InsertarEnPrestamos(oPrestamo, esMensajero, !esMensajero);
            if (seGuardo)
            {
                MessageBox.Show("Préstamo guardado exitosamente.");
            }
            else
            {
                MessageBox.Show("Hubo un error guardando el préstamo.");
            }
        }

        private void rbMensajero_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT MENSAJEROS.IdTrabajador, MENSAJEROS.nombre, MENSAJEROS.estado
            FROM MENSAJEROS
            WHERE (((MENSAJEROS.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, conexion.ConexionDbInterna());
            lbxTrabajadores.DataSource = lista;
            lbxTrabajadores.DisplayMember = "nombre";
            lbxTrabajadores.ValueMember = "IdTrabajador";
            listarConceptos();
        }

        private void rbTrabajador_CheckedChanged(object sender, EventArgs e)
        {
            string consulta = @"SELECT TRABAJADORES.IdTrabajador, TRABAJADORES.nombre, TRABAJADORES.estado
            FROM TRABAJADORES
            WHERE (((TRABAJADORES.estado)=True));";
            DataTable lista = new SentenciaSqlOLEDB().TraerDatos(consulta, conexion.ConexionDbInterna());


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
            else if (rbTrabajador.Checked)
            {
                sql = "select Id,Concepto from ConceptoAdelanto where activo = 1 and ( Permiso=3 or Permiso=1)";
            }
            else
            {
                sql = "select Id,Concepto from ConceptoAdelanto where activo=1 and permiso=1";
            }
            DataTable ListaConceptosPrestamo = new SentenciaSqlServer().TraerDatos(sql, conexion.ConexionCierreCaja());
            cbConceptos.DataSource = ListaConceptosPrestamo;

            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";

        }

        private void CargarTodosConceptos()
        {

            string consulta = "select Id,Concepto from ConceptoAdelanto where activo=1";
            DataTable lista = new SentenciaSqlServer().TraerDatos(consulta, conexion.ConexionCierreCaja());
            cbConceptos.DataSource = lista;
            cbConceptos.DisplayMember = "Concepto";
            cbConceptos.ValueMember = "Id";
        }

        private void rbMensajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                lbxTrabajadores.Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                rbTrabajador.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void rbTrabajador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                lbxTrabajadores.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                rbMensajero.Focus();
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

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbConceptos.Focus();
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

        }

        private void btnGuardar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int idMov = 0;
                Movimiento oMovimiento = new Movimiento();

                oMovimiento.IdUsuario = admin.idUsuario;
                oMovimiento.Descripcion = txtObservaciones.Text;
                oMovimiento.Valor = Convert.ToDecimal(txtValor.Text);
                oMovimiento.IdConcepto = 9;

                idMov = new PrestamosRepository().Insertar(oMovimiento);


                Prestamo oPrestamo = new Prestamo();

                if (rbMensajero.Checked)
                {
                    oPrestamo.IdMensajero = lbxTrabajadores.SelectedValue.ToString();
                }
                else if (rbTrabajador.Checked)
                {
                    oPrestamo.IdTrabajador = lbxTrabajadores.SelectedValue.ToString();
                }

                oPrestamo.Valor = Convert.ToDecimal(txtValor.Text);
                oPrestamo.Concepto = cbConceptos.Text;
                oPrestamo.Observacion = txtObservaciones.Text;
                oPrestamo.Cajero = admin.idUsuario;
                oPrestamo.Caja = 0.ToString();
                oPrestamo.IdMovimiento = idMov;


                bool esMensajero = rbMensajero.Checked;

                bool seGuardo = new PrestamosRepository().InsertarEnPrestamos(oPrestamo, esMensajero, !esMensajero);
                if (seGuardo)
                {
                    MessageBox.Show("Préstamo guardado exitosamente.");
                }
                else
                {
                    MessageBox.Show("Hubo un error guardando el préstamo.");
                }
            }
        }
    }
}
