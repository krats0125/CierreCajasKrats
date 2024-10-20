using CierreDeCajas.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.Operativo
{
    public partial class FrmVentas : Form
    {

        Principal ppal = null;
        public decimal TotalVentas = 0;
       
        public FrmVentas(Principal ppal)
        {
            InitializeComponent();
            this.ppal = ppal;
        }

        private void txtVentas_TextChanged(object sender, EventArgs e)
        {

            if (decimal.TryParse(txtVentas.Text, out decimal totalVentas))
            {
                if (totalVentas == 0)
                {
                    return;
                }
                TotalVentas = totalVentas;

                
                    FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                    if (frm != null)
                    {
                        frm.CargarCierreVentas();
                        bool actualizacionExitosa = new CierreCajaRepository().ActualizarCierre(ppal.idCierre, TotalVentas);
                        if (!actualizacionExitosa)
                        {
                            MessageBox.Show("Hubo un error actualizando el cierre de caja");
                        }
                    }
                
            }
        }

      
    }
}
