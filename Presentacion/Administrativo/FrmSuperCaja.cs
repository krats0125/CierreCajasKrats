using CierreDeCajas.Presentacion.Sistema;
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
    public partial class FrmSuperCaja : Form
    {
        FrmLogin lgn = new FrmLogin();
        public string idUsuario;
        public int idCierre;
        public DateTime Fecha = DateTime.Now;
        public FrmSuperCaja(FrmLogin login)
        {
            InitializeComponent();
            lgn = login;
        }

        private void FrmSuperCaja_Load(object sender, EventArgs e)
        {
            idUsuario = lgn.idUsuario;
            idCierre = lgn.idCierre;


            //Se inicia el Timer
            TimerHora.Enabled = true;


            AbrirFormularioEnPanel<FrmResumenSuperCaja>(this);
            AbrirFormularioEnPanel<FrmMovimientosSuperCaja>(this);
            //AbrirFormularioEnPanel<FrmMenuda>(this);

            lb_Cajero.Text = lgn.NombreUsuario;
            lbIdCierre.Text = idCierre.ToString();
        }

        private Dictionary<Type, Form> InstanciaDelFormulario = new Dictionary<Type, Form>();

        private void AbrirFormularioEnPanel<T>(FrmSuperCaja supercaja) where T : Form
        {
            Type formType = typeof(T);

            // Verificar si ya existe una instancia del formulario
            if (InstanciaDelFormulario.ContainsKey(formType))
            {
                Form ExisteFormulario = InstanciaDelFormulario[formType];
                if (!ExisteFormulario.IsDisposed)
                {
                    ExisteFormulario.BringToFront(); // Traer el formulario al frente si está oculto detrás de otros controles
                    return;
                }
                // Si el formulario está desechado, eliminar la instancia
                InstanciaDelFormulario.Remove(formType);
            }

            // Crear una nueva instancia del formulario con el formulario Principal como parámetro
            Form FormularioHijo = (Form)Activator.CreateInstance(typeof(T), supercaja); // Usar Activator para pasar parámetros
            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;

            // Agregar el formulario al panel
            PanelMaestro.Controls.Add(FormularioHijo);
            FormularioHijo.Show();

            // Almacenar la instancia del formulario en el diccionario
            InstanciaDelFormulario.Add(formType, FormularioHijo);
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            lb_FechaActual.Text = Fecha.ToString();
        }

        private void FrmSuperCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerHora.Enabled = false;
            Application.Exit();
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel<FrmResumenSuperCaja>(this);
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
           AbrirFormularioEnPanel<FrmMovimientosSuperCaja>(this);
        }

        private void btnMenuda_Click(object sender, EventArgs e)
        {
           AbrirFormularioEnPanel<FrmMenudaSuperCaja>(this);
        }
    }
}
