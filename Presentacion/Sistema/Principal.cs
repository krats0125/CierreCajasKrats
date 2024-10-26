using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Sistema;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion
{
    public partial class Principal : Form
    {
        FrmLogin lgn = new FrmLogin();


        public string idUsuario;
        public int idCaja;
        public int idCierre;

        public Principal(FrmLogin login)
        {
           lgn = login;
            InitializeComponent();

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
            //AbrirFormularioEnPanel<FrmPrestamos>(this);
            AbrirFormularioEnPanel<FrmMenuda>(this);
            //AbrirFormularioEnPanel<FrmVentas>(this);

            btnAdelantos.Visible = false;



            lb_Caja.Text = lgn.Caja;
            lb_Cajero.Text = lgn.NombreUsuario;
            lbIdCierre.Text =idCierre.ToString();


        }



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


        private void TimerHora_Tick(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Now;
            lb_FechaActual.Text = Fecha.ToString();
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerHora.Enabled = false;
            Application.Exit();

        }


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

        private void BarraS_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCargarDomicilios_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos CSV|*.csv", // Cambiar el filtro para archivos CSV
                 Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Almacenar el resultado de la inserción
                bool insercionExitosa = true;

                foreach (string rutaArchivo in openFileDialog.FileNames) // Iterar sobre los archivos seleccionados
                {
                    try
                    {
                        // Leer el archivo CSV y obtener los domicilios
                        DomicilioRepository domicilioRepo = new DomicilioRepository(this);
                        bool resultado = domicilioRepo.LeerExcel(rutaArchivo);

                        // Verificar si la inserción fue exitosa
                        if (!resultado)
                        {
                            insercionExitosa = false; // Si algún archivo no se insertó correctamente
                        }
                    }
                    catch (Exception ex)
                    {
                        // Captura cualquier error que pueda ocurrir durante el proceso
                        MessageBox.Show($"Ocurrió un error al procesar el archivo {rutaArchivo}: {ex.Message}");
                        insercionExitosa = false; // Actualizar el estado en caso de error
                    }
                }

                // Mostrar mensaje final
                if (insercionExitosa)
                {
                    MessageBox.Show("Todos los datos insertados exitosamente.");
                    FrmMovimientos frmMvt = new FrmMovimientos(this);
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
                else
                {
                    MessageBox.Show("Algunos datos no se pudieron insertar.");
                }
            }
        }
    
    }
}
