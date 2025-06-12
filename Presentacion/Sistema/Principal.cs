using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion
{
    public partial class Principal : Form
    {
        FrmLogin lgn = new FrmLogin();
        private FrmMovimientos frmMvt;

        public string idUsuario;
        public int idCaja;
        public int idCierre;
        public DateTime Fecha = DateTime.Now;

        public Principal(FrmLogin login)
        {
           lgn = login;
            InitializeComponent();
            frmMvt = new FrmMovimientos(this);

        }
        private void Principal_Load_1(object sender, EventArgs e)
        {
            //Se Asignan las variables globales
            idUsuario = lgn.idUsuario;
            idCaja = lgn.idCaja;
            idCierre = lgn.idCierre;


            //Se inicia el Timer
           TimerHora.Enabled = true;

            AbrirFormularioEnPanel<FrmCierreCaja>(this);
            AbrirFormularioEnPanel<FrmMovimientos>(this);
            AbrirFormularioEnPanel<FrmPrestamos>(this);
            AbrirFormularioEnPanel<FrmMenuda>(this);
            //AbrirFormularioEnPanel<FrmVentas>(this);

            //btnAdelantos.Visible = false;


            lb_Caja.Text = lgn.Caja;
            lb_Cajero.Text = lgn.NombreUsuario;
            lbIdCierre.Text =idCierre.ToString();

            cargarTransferencias();
            cargarDatafonos();
            cargarBonos();
            cargarVentasRappi();
        }

        #region Panel padre para los formularios
        private Dictionary<Type, Form> InstanciaDelFormulario = new Dictionary<Type, Form>();
        private void AbrirFormularioEnPanel1<T>() where T : Form, new()
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

            // Crear una nueva instancia del formulario
            T FormularioHijo = new T();
            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;

            // Agregar el formulario al panel
            PanelMaestro.Controls.Add(FormularioHijo);
            FormularioHijo.Show();

            // Almacenar la instancia del formulario en el diccionario
            InstanciaDelFormulario.Add(formType, FormularioHijo);
        }

        private void AbrirFormularioEnPanel<T>(Principal principal) where T : Form
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
            Form FormularioHijo = (Form)Activator.CreateInstance(typeof(T), principal); // Usar Activator para pasar parámetros
            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;

            // Agregar el formulario al panel
            PanelMaestro.Controls.Add(FormularioHijo);
            FormularioHijo.Show();

            // Almacenar la instancia del formulario en el diccionario
            InstanciaDelFormulario.Add(formType, FormularioHijo);
        }
        #endregion

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            string fecha = DateTime.Now.ToString();
            lb_FechaActual.Text = fecha;
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerHora.Enabled = false;
            Application.Exit();

        }
        #region Botones del menú
        private void btnResumen_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel<FrmCierreCaja>(this);
           
        }

        private void btnMenuda_Click(object sender, EventArgs e)
        {
            
            AbrirFormularioEnPanel<FrmMenuda>(this);
           

        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
           
            AbrirFormularioEnPanel<FrmMovimientos>(this);

            
        }

        private void btnAdelantos_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel<FrmPrestamos>(this);
            
        }
        private void btnCargarDomicilios_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos CSV|*.csv",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Instanciar el repositorio y pasarle la lista de archivos
                DomicilioRepository domicilioRepo = new DomicilioRepository(this);
                List<string> rutasArchivos = openFileDialog.FileNames.ToList();

                try
                {
                    // Leer todos los archivos seleccionados en una sola llamada
                    bool insercionExitosa = domicilioRepo.LeerExcel(rutasArchivos);


                    if (insercionExitosa)
                    {
                        MessageBox.Show("Todos los datos insertados exitosamente.");

                        frmMvt.ListaMovimientos();
                        FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                        frm.CargarSumatorias();
                        frm.CitarPanelesMovimientos();

                        bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
                        if (!actualizacionExitosa)
                        {
                            MessageBox.Show("Hubo un error actualizando el cierre de caja.");
                        }

                        frm.CargarCierreVentas();
                    }
                    else
                    {
                        MessageBox.Show("Algunos datos no se pudieron insertar. Asegúrate de cerrar el archivo y vuelve a intentarlo.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al procesar los archivos: {ex.Message}");
                }
            }
        }
        #endregion

        private void BarraS_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Cargar valores del sistema 
        private void cargarTransferencias()
        {
            var transferenciasCargadas = frmMvt.traerTransferencias();

            
            bool insercionExitosa = frmMvt.InsetarTransferencias(transferenciasCargadas);

            if (insercionExitosa)
            {

                
                    frmMvt.ListaMovimientos();
                

                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();

                bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                frm.CargarCierreVentas();
            }
            
        }

        private void cargarVentasRappi()
        {
            var VentasRappiCargadas = frmMvt.traerVentasRappi();
            //frmMvt.EliminarDevolucionesRappi();
            bool insercionExitosa = frmMvt.InsetarVentasRappi(VentasRappiCargadas);

            if (insercionExitosa)
            {


                frmMvt.ListaMovimientos();


                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();

                bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                frm.CargarCierreVentas();
            }

        }
        private void cargarDatafonos()
        {
            var datafonosCargados = frmMvt.traerDatafonos();


            bool insercionExitosa = frmMvt.InsertarDatafonos(datafonosCargados);

            if (insercionExitosa)
            {

                
                    frmMvt.ListaMovimientos();
                

                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();

                bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                frm.CargarCierreVentas();
            }

        }

        private void cargarBonos()
        {
            var BonosCargados = frmMvt.traerBonos();


            bool insercionExitosa = frmMvt.InsertarBonoAlcadia
                (BonosCargados);

            if (insercionExitosa)
            {


                frmMvt.ListaMovimientos();


                FrmCierreCaja frm = new InstanciasRepository().InstanciaFrmCierredeCaja();
                frm.CargarSumatorias();
                frm.CitarPanelesMovimientos();

                bool actualizacionExitosa = new CierreCajaRepository(this).ActualizarCierre(idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                frm.CargarCierreVentas();
            }

        }
        #endregion
      
    
    }
}
