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
    public partial class FrmAdelantos : Form
    {
        FrmAdministrativo admin = null;
        public FrmAdelantos(FrmAdministrativo admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void CGPAdelantos_Click(object sender, EventArgs e)
        {
            FrmNuevoAdelanto adelanto=new FrmNuevoAdelanto(admin);
            adelanto.Show();
        }

        private void lbAdelantos_Click(object sender, EventArgs e)
        {
            FrmNuevoAdelanto adelanto = new FrmNuevoAdelanto(admin);
            adelanto.Show();
        }

        private void pbAdelantos_Click(object sender, EventArgs e)
        {
            FrmNuevoAdelanto adelanto = new FrmNuevoAdelanto(admin);
            adelanto.Show();
        }

      

        private void CGPConcepto_Click(object sender, EventArgs e)
        {
            FrmCrearNuevoConcepto concepto = new FrmCrearNuevoConcepto();
            concepto.Show();
        }

        private void lbConcepto_Click(object sender, EventArgs e)
        {
            FrmCrearNuevoConcepto concepto = new FrmCrearNuevoConcepto();
            concepto.Show();
        }

        private void pbConcepto_Click(object sender, EventArgs e)
        {
            FrmCrearNuevoConcepto concepto = new FrmCrearNuevoConcepto();
            concepto.Show();
        }

        private void cgpEliminarAdelanto_Click(object sender, EventArgs e)
        {
            FrmEliminarAdelanto eliminar = new FrmEliminarAdelanto();
            eliminar.Show();
        }

        private void lbEliminarAdelanto_Click(object sender, EventArgs e)
        {
            FrmEliminarAdelanto eliminar = new FrmEliminarAdelanto();
            eliminar.Show();
        }

        private void pbEliminarAdelanto_Click(object sender, EventArgs e)
        {
            FrmEliminarAdelanto eliminar = new FrmEliminarAdelanto();
            eliminar.Show();
        }
    }
}
