
using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Sistema;
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

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmDetalleReporte : Form
    {
        CONEXION cn = new CONEXION();
        public int IdCierre;
        public string IdUsuario;
        private FrmMovimientosAdmin FrmMovimientosAdmin;
        private DateTime fechaApertura;
        public int idcaja;
        public FrmDetalleReporte(int IdCierre, string idUsuario, DateTime fechaApertura)
        {
            InitializeComponent();
            this.IdCierre = IdCierre;
            this.IdUsuario = idUsuario;
            FrmMovimientosAdmin = new FrmMovimientosAdmin(this, fechaApertura);
            this.fechaApertura = fechaApertura;
        }

        private void FrmDetalleReporte_Load(object sender, EventArgs e)
        {
            CargarSumatorias();
            cargarVentas();
            CargarCierreVentas();
            CitarPanelesMovimientos();
            cargarNovedades();
            lb_Cajero.Text = IdUsuario;
            cargarDatafonos();

            cargarTransferencias();
            TraerCaja();
            cargarBonos();
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
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_listarSumariaValoresPorCierreCajero", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@IdCierre",IdCierre));

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
                    conexion.Close();
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
            DetalleReporteRepository repository = new DetalleReporteRepository(this,fechaApertura);

            CierreCaja oCierreCaja = repository.listar(IdCierre, out mensaje);



            lb_TotalEfectivo.Text = oCierreCaja.TotalEfectivo.ToString("C0");
            lb_Diferencia.Text = oCierreCaja.Diferencia.ToString("C0");
            lb_TotalDatafono.Text = oCierreCaja.TotalDatafono.ToString("C0");
            lb_TotalLiquidado.Text = oCierreCaja.TotalLiquidado.ToString("C0");
            lb_TotalMovimientosCaja.Text = oCierreCaja.TotalMovimientosCaja.ToString("C0");
            lb_TotalTransferencia.Text = oCierreCaja.TotalTransferencia.ToString("C0");
            lb_ValorVentas.Text = oCierreCaja.ValorVentas.ToString("C0");
            lb_entregaultimoefectivo.Text = oCierreCaja.EntregaUltimoEfectivo.ToString("C0");
        }

        public void ActualizarCiereCaja()
        {

            bool actualizacionExitosa = new DetalleReporteRepository(this,fechaApertura).ActualizarCierre(IdCierre);
            if (!actualizacionExitosa)
            {
                MessageBox.Show("Hubo un error actualizando el cierre de caja");
            }

        }
        private void cargarVentas()
        {
            DetalleReporteRepository detallerepo = new DetalleReporteRepository(this, fechaApertura);

            decimal totalVentas = detallerepo.ActualizarVentas(IdUsuario);


            if (totalVentas > 0)
            {
                lb_ValorVentas.Text = totalVentas.ToString("C0");


                bool actualizacionExitosa = detallerepo.ActualizarCierre(IdCierre);

                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                else
                {

                    CargarCierreVentas();
                }
            }
        }

        private void cargarNovedades()
        {
            DetalleReporteRepository detallerepo = new DetalleReporteRepository(this, fechaApertura);
            string novedades=detallerepo.CargarNovedades();
            if (novedades != null)
            {
                lbNovedades.Visible = true;
                txtnotas.Visible = true;
                txtnotas.Text = novedades;
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
            from MovimientoCaja MC 
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
                    Height = 150,
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

    

        public int TraerCaja()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = "select top 1 idcaja from MovimientoCaja where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            idcaja = Convert.ToInt32(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return idcaja;
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientosAdmin movimientos = new FrmMovimientosAdmin(this, fechaApertura);
            movimientos.Show();
        }

        private void btnMenuda_Click(object sender, EventArgs e)
        {
            FrmMenudaAdmin menuda = new FrmMenudaAdmin(this, fechaApertura);
            menuda.Show();
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
                DetalleReporteRepository Repo = new DetalleReporteRepository(this, fechaApertura    );
                List<string> rutasArchivos = openFileDialog.FileNames.ToList();

                try
                {
                    // Leer todos los archivos seleccionados en una sola llamada
                    bool insercionExitosa = Repo.LeerExcel(rutasArchivos);


                    if (insercionExitosa)
                    {
                        MessageBox.Show("Todos los datos insertados exitosamente.");

                        FrmMovimientosAdmin.ListaMovimientos(); ;
                        CargarSumatorias();
                        CitarPanelesMovimientos();
                        DetalleReporteRepository detallerepo = new DetalleReporteRepository(this,fechaApertura);
                        bool actualizacionExitosa = detallerepo.ActualizarCierre(IdCierre);

                        if (!actualizacionExitosa)
                        {
                            MessageBox.Show("Hubo un error actualizando el cierre de caja");
                        }
                        else
                        {

                            CargarCierreVentas();
                        }
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

        private void txtnotas_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void cargarTransferencias()
        {
            var transferenciasCargadas = FrmMovimientosAdmin.traerTransferencias();


            bool insercionExitosa = FrmMovimientosAdmin.InsetarTransferencias(transferenciasCargadas);

            if (insercionExitosa)
            {


                FrmMovimientosAdmin.ListaMovimientos();



                CargarSumatorias();
                CitarPanelesMovimientos();

                DetalleReporteRepository detallerepo = new DetalleReporteRepository(this, fechaApertura);
                bool actualizacionExitosa = detallerepo.ActualizarCierre(IdCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                CargarCierreVentas();
            }

        }

        private void cargarDatafonos()
        {
            var datafonosCargados = FrmMovimientosAdmin.traerDatafonos();


            bool insercionExitosa = FrmMovimientosAdmin.InsertarDatafonos(datafonosCargados);

            if (insercionExitosa)
            {


                FrmMovimientosAdmin.ListaMovimientos();



                CargarSumatorias();
                CitarPanelesMovimientos();

                DetalleReporteRepository detallerepo = new DetalleReporteRepository(this, fechaApertura);
                bool actualizacionExitosa = detallerepo.ActualizarCierre(IdCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                CargarCierreVentas();
            }

        }

        private void cargarBonos()
        {
            var bonosCargados = FrmMovimientosAdmin.traerBonos();


            bool insercionExitosa = FrmMovimientosAdmin.InsertarBonoAlcadia(bonosCargados);

            if (insercionExitosa)
            {


                FrmMovimientosAdmin.ListaMovimientos();



                CargarSumatorias();
                CitarPanelesMovimientos();

                DetalleReporteRepository detallerepo = new DetalleReporteRepository(this, fechaApertura);
                bool actualizacionExitosa = detallerepo.ActualizarCierre(IdCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }

                CargarCierreVentas();
            }

        }
    }
}
