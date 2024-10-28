using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmCrearNuevoConcepto : Form
    {
        CONEXION Conexion = new CONEXION();
        public FrmCrearNuevoConcepto()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ConceptoAdelanto oConcepto= new ConceptoAdelanto();

            oConcepto.Concepto=txtConcepto.Text;
            oConcepto.Descripcion=txtDescripcion.Text;
            
            bool mensajero=rbMensajero.Checked;
            bool trabajador=rbTrabajador.Checked;
            bool ambos=rbAmbos.Checked;

            bool seInserto=new ConceptoAdelantoRepository().Insertar(oConcepto,mensajero,trabajador,ambos);
            if (seInserto)
            {
                MessageBox.Show("Concepto guardado exitosamente.");
            }
            else
            {
                MessageBox.Show("Hubo un error guardando el Concepto.");
            }
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;
                txtDescripcion.Focus();
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rbMensajero.Focus();
            }
        }

        private void rbMensajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuardar.Focus();
            }
            else if (e.KeyCode == Keys.Right) 
            {
                e.SuppressKeyPress = true; 
                rbTrabajador.Focus(); 
            }
        }

        private void rbTrabajador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuardar.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Mover el foco al radio button Mensajero
                rbMensajero.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                // Mover el foco al radio button Ambos
                rbAmbos.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void rbAmbos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGuardar.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Mover el foco al radio button Mensajero
                rbMensajero.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                // Mover el foco al radio button Ambos
                rbAmbos.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
