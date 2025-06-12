using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion.AdminSuperCaja
{
    public partial class FrmResumenSuperCajaAdmin : Form
    {
        CONEXION Conexion = new CONEXION();
        decimal valorentregado;
        public int IdCierre;
        public string IdUsuario;
        private DateTime fechaApertura;
        public decimal totalEfectivoSistema;
        public decimal totalDatafonoSistema;
        public FrmResumenSuperCajaAdmin(int IdCierre, string idUsuario, DateTime fechaApertura)
        {
            InitializeComponent();
            this.IdCierre = IdCierre;
            this.IdUsuario = idUsuario;
            this.fechaApertura = fechaApertura;
        }
        private void FrmResumenSuperCajaAdmin_Load(object sender, EventArgs e)
        {
            CargarSumatorias();
            CargarCierreVentas();
            CitarPanelesMovimientos();
            cargarNovedades();

        }

        private void CrearPanelesConMediosDePago(List<MedioDePago> mediosPago)
        {
            pnlflListaMedioPago.Controls.Clear(); // Limpiar panel antes de agregar nuevos elementos

            foreach (var medio in mediosPago)
            {
                // Crear un nuevo panel
                Panel panel = new Panel
                {
                    Width = pnlflListaMedioPago.Width - 20, // Ajustar el tamaño del panel
                    Height = 30, // Altura del panel
                    Margin = new Padding(2) // Margen entre los paneles
                };

                // Crear el primer label para la descripción
                Label labelDescripcion = new Label
                {
                    Text = medio.Descripcion,
                    AutoSize = true, // Ajustar automáticamente el tamaño del label
                    Location = new Point(10, 10), // Posición dentro del panel
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular)
                };

                // Crear el segundo label para el valor
                Label labelValor = new Label
                {
                    Text = medio.Valor.ToString("C0"), // Formatear como moneda
                    AutoSize = true,
                    Location = new Point(230, 10), // Posición dentro del panel
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleRight
                };

                // Agregar labels al panel
                panel.Controls.Add(labelDescripcion);
                panel.Controls.Add(labelValor);

                // Agregar el panel al panel principal
                pnlflListaMedioPago.Controls.Add(panel);
                pnlflListaMedioPago.Refresh();
            }
        }


        private List<MedioDePago> listarSumatoriaMedioDePago()
        {
            List<MedioDePago> mediosPago = new List<MedioDePago>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("listarSumariaValoresPorCierreSuperCaja", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario",IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@IdCierre", IdCierre));

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                mediosPago.Add(new MedioDePago()
                                {
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"].ToString())
                                });

                            }
                        }


                    }
                    cn.Close();
                }

            }
            catch (Exception)
            {


                return mediosPago;

            }



            return mediosPago;

        }

        public void CargarSumatorias()
        {

            List<MedioDePago> lista = listarSumatoriaMedioDePago();
            if (lista.Count > 0)
            {
                CrearPanelesConMediosDePago(lista);
            }
            else
            {
                pnlflListaMedioPago.Controls.Clear();
            }


        }
        public void CargarCierreVentas()
        {
            string mensaje;
            SuperCajaAdminRepository repository = new SuperCajaAdminRepository(this);

            CierreSuperCaja oCierreSuperCaja = repository.listar(IdCierre, out mensaje);

            if (oCierreSuperCaja != null)
            {
                lb_TotalEfectivo.Text = oCierreSuperCaja.TotalEfectivo.ToString("C0");
                lbEfectivoSistema.Text = oCierreSuperCaja.TotalEfectivoSistema.ToString("C0");
                lbDiferenciaEfectivo.Text = oCierreSuperCaja.DiferenciaEfectivo.ToString("C0");
                lbTotalCobrado.Text = oCierreSuperCaja.TotalCobrado.ToString("C0");
                lb_TotalDatafono.Text = oCierreSuperCaja.TotalDatafono.ToString("C0");
                lbDatafonosSistema.Text = oCierreSuperCaja.TotalDatafonoSistema.ToString("C0");
                lbDiferenciaDatafonos.Text = oCierreSuperCaja.DiferenciaDatafono.ToString("C0");
                lb_TotalLiquidado.Text = oCierreSuperCaja.TotalLiquidado.ToString("C0");
                lb_TotalMovimientosCaja.Text = oCierreSuperCaja.TotalMovimientosCaja.ToString("C0");
                lb_entregaultimoefectivo.Text = oCierreSuperCaja.EntregaUltimoEfectivo.ToString("C0");
                lbDiferenciastotales.Text = oCierreSuperCaja.Diferencia.ToString("C0");

            }
        }

        public class MovimientoList
        {
            public string Concepto { get; set; }
            public string Descripcion { get; set; }
            public string Valor { get; set; }
        }

        public void CitarPanelesMovimientos()
        {
            string consulta = $@"Select MC.IdMovimiento, CM.Concepto, MC.Descripcion, MC.Valor, MP.Descripcion, MC.fecha
            from movimientosupercaja MC 
            inner join ConceptoMovimiento CM ON MC.IdConcepto= CM.Id 
            inner join MediosDePago MP ON MC.IdMedioPago=MP.IdMedioPago
            where MC.IdUsuario='{IdUsuario}' and MC.IdCierre= {IdCierre}
			ORDER BY CM.Concepto";

            List<MovimientoList> movimientos = new List<MovimientoList>();

            CONEXION cn = new CONEXION();
            using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            movimientos.Add(

                                new MovimientoList
                                {
                                    Concepto = dr["Concepto"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"]).ToString("C0")
                                }

                            );

                        }
                    }
                }
            }
            GenerarPanelesMovimientos(movimientos);

        }




        public void GenerarPanelesMovimientos(List<MovimientoList> movimientos)
        {
            // Agrupar movimientos por concepto
            var grupos = movimientos.GroupBy(m => m.Concepto);

            // Limpiar el panel antes de agregar nuevos controles
            panelesMovimientos.Controls.Clear();

            foreach (var grupo in grupos)
            {
                // Crear un nuevo panel para cada grupo
                Panel panel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 300, // Ancho del panel
                    Height = 130,
                    Margin = new Padding(1), // Margen alrededor del panel
                    AutoScroll = true,
                };

                // Crear un contenedor para las descripciones y valores
                FlowLayoutPanel innerPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    AutoScroll = true,
                    WrapContents = false
                };

                Label labelTitulo = new Label
                {
                    Text = grupo.Key,
                    Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(84, 178, 231),
                    AutoSize = true
                };

                innerPanel.Controls.Add(labelTitulo); // Añadir el título antes de los valores

                // Agregar los movimientos del grupo al contenedor
                foreach (var movimiento in grupo)
                {
                    FlowLayoutPanel valuePanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight, // Cambiar para alinear los controles horizontalmente
                        AutoSize = true,
                        Margin = new Padding(0, 5, 0, 5), // Espaciado entre movimientos

                    };

                    Label labelValor = new Label
                    {
                        Text = movimiento.Valor,
                        Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                        Width = 100, // Ancho del panel
                        ForeColor = Color.White,
                        AutoSize = true,
                        TextAlign = ContentAlignment.TopLeft,
                    };

                    Label labelDescripcion = new Label
                    {
                        Text = movimiento.Descripcion,
                        Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                        Width = 210,
                        ForeColor = Color.White,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft
                    };

                    // Agregar primero el valor y luego la descripción
                    valuePanel.Controls.Add(labelValor);       // Añadir el valor primero
                    valuePanel.Controls.Add(labelDescripcion); // Luego la descripción

                    innerPanel.Controls.Add(valuePanel); // Añadir el panel de valor-descripción al contenedor
                }

                panel.Controls.Add(innerPanel);
                panelesMovimientos.Controls.Add(panel);
            }
        }



        private void cargarNovedades()
        {
            SuperCajaAdminRepository supercajarepo = new SuperCajaAdminRepository(this);
            string novedades = supercajarepo.CargarNovedades();
            if (novedades != null)
            {
                lbNovedades.Visible = true;
                txtnotas.Visible = true;
                txtnotas.Text = novedades;
            }

        }
 

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientosSuperCajaAdmin movimientos = new FrmMovimientosSuperCajaAdmin(this);
            movimientos.Show();

        }

        private void btnMenuda_Click(object sender, EventArgs e)
        {
            FrmMenudaSuperCajaAdmin menuda = new FrmMenudaSuperCajaAdmin(this);
            menuda.Show();
        }

        private void btnSistema_Click(object sender, EventArgs e)
        {
            CierreSuperCaja oCierresupercaja = new CierreSuperCaja();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos CSV|*.csv",
           
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SuperCajaAdminRepository repo = new SuperCajaAdminRepository(this);
                List<string> rutasArchivos = openFileDialog.FileNames.ToList();

                try
                {
                    // Leer y sumar valores de los archivos
                    bool lecturaExitosa = repo.LeerYSumarValores(rutasArchivos);

                    if (lecturaExitosa)
                    {
                        totalEfectivoSistema = repo.EfectivoSistema;
                        totalDatafonoSistema = repo.datafonoSistema;
                        bool insercionExitosa = repo.InsertarEnBaseDeDatos(oCierresupercaja,fechaApertura);

                        bool actualizacionExitosa = new SuperCajaAdminRepository(this).ActualizarCierre(IdCierre);

                        if (insercionExitosa)
                        {
                            MessageBox.Show("Datos insertados exitosamente.");


                            if (!actualizacionExitosa)
                            {
                                MessageBox.Show("Hubo un error actualizando el cierre de caja.");
                            }
                            CargarCierreVentas();
                        }
                        else
                        {
                            MessageBox.Show("Error al insertar datos en la base de datos.");
                        }


                    }
                    else
                    {
                        MessageBox.Show("Error, por favor cierre el archivo e intentelo de nuevo.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}");
                }
            }
        }
    }
}
