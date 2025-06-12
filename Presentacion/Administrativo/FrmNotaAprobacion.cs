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
    public partial class FrmNotaAprobacion : Form
    {
        FrmDetalleReporte detalle=null;
        string nota = "";
        public FrmNotaAprobacion(FrmDetalleReporte detalle)
        {
            InitializeComponent();
            this.detalle = detalle;
        }

        private void FrmNotaAprobacion_Load(object sender, EventArgs e)
        {
            cargarNotaAprobacion();
        }
        private void cargarNotaAprobacion()
        {
            NotaAprobacionRepository repo = new NotaAprobacionRepository();
            NotaAprobacion oNota = new NotaAprobacion();
            string notaAprobacion = repo.mostrarNota(detalle.IdCierre);
            if (notaAprobacion != null && notaAprobacion != "")
            {
                txtDescripcionAprobacion.Text = notaAprobacion;
            }

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            NotaAprobacionRepository repo = new NotaAprobacionRepository();
            int nota = repo.ObtenerNotaPorIdCierre(detalle.IdCierre);

            if (nota == 0)
            {
                NotaAprobacion oNota = new NotaAprobacion
                {
                    IdCierre = detalle.IdCierre,
                    Descripcion = txtDescripcionAprobacion.Text
                };
                bool seInserto = repo.InsertarNota(oNota);
                if (seInserto)
                {
                    MessageBox.Show("Nota insertada correctamente.");
                }
                else
                {
                    MessageBox.Show("Error al insertar la nota.");
                }
            }
            else
            {
                NotaAprobacion oNota = new NotaAprobacion();
                oNota.IdCierre = detalle.IdCierre;
                oNota.Descripcion = txtDescripcionAprobacion.Text;
                bool seActualizo = repo.ActualizarNota(oNota);
                if (seActualizo)
                {
                    MessageBox.Show("Nota actualizada correctamente.");
                    repo.RestablecerNotaMostrada(oNota.IdCierre);
                }
                else
                {
                    MessageBox.Show("Error al actualizar la nota.");
                }
            }

          detalle.cargarNotaAprobacion();
          this.Close();
        }


    }
}
