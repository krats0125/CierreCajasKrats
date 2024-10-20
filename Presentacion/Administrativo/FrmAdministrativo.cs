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
    public partial class FrmAdministrativo : Form
    {
        FrmLogin lgn = new FrmLogin();
        public string idUsuario;

        public FrmAdministrativo(FrmLogin login)
        {
            lgn= login;
            InitializeComponent();
        }

        private void FrmAdministrativo_Load(object sender, EventArgs e)
        {

            idUsuario = lgn.idUsuario;
            TimerHora.Enabled = true;

            lb_cajero.Text = lgn.NombreUsuario;
        }

    

        private void CGPTrabajadores_Click(object sender, EventArgs e)
        {
            FrmTrabajador trabajador = new FrmTrabajador();
            trabajador.Show();
        }

        private void lbTrabajador_Click(object sender, EventArgs e)
        {
            FrmTrabajador trabajador = new FrmTrabajador();
            trabajador.Show();
        }

        private void pbTrabajador_Click(object sender, EventArgs e)
        {
            FrmTrabajador trabajador = new FrmTrabajador();
            trabajador.Show();
        }

        private void CGPReportes_Click(object sender, EventArgs e)
        {
            FrmReportes reporte=new FrmReportes();
            reporte.Show();
        }

        private void lbReportes_Click(object sender, EventArgs e)
        {
            FrmReportes reporte = new FrmReportes();
            reporte.Show();
        }

        private void pbReportes_Click(object sender, EventArgs e)
        {
            FrmReportes reporte = new FrmReportes();
            reporte.Show();
        }

        private void cgpAdelantos_Click(object sender, EventArgs e)
        {
            FrmAdelantos adelantos = new FrmAdelantos(this);
            adelantos.Show();
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Now;
            lb_FechaActual.Text = Fecha.ToString();
        }

        private void FrmAdministrativo_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerHora.Enabled = false;
            Application.Exit();
        }

        private void pbMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientosAdmin movimientos = new FrmMovimientosAdmin();
            movimientos.Show();
        }

        private void CGPMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientosAdmin movimientos = new FrmMovimientosAdmin();
            movimientos.Show();
        }

        private void lbMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientosAdmin movimientos = new FrmMovimientosAdmin();
            movimientos.Show();
        }

     

        private void lbAdelantos_Click(object sender, EventArgs e)
        {
            FrmAdelantos adelantos = new FrmAdelantos(this);
            adelantos.Show();
        }

        private void pbAdelantos_Click(object sender, EventArgs e)
        {
            FrmAdelantos adelantos = new FrmAdelantos(this);
            adelantos.Show();
        }
    }
}
