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
    }
}
