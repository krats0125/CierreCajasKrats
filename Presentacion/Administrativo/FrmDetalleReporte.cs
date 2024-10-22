using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
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
        Principal ppal;
        private int IdCierre;
        private string IdUsuario;
        public FrmDetalleReporte(int IdCierre, string idUsuario)
        {
            InitializeComponent();
            this.IdCierre = IdCierre;
            this.IdUsuario = idUsuario;
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
                    Height = 25, // Altura del panel
                    Margin = new Padding(1) // Margen entre los paneles
                };

                // Crear el primer label para la descripción
                Label labelDescripcion = new Label
                {
                    Text = medio.Descripcion,
                    AutoSize = true, // Ajustar automáticamente el tamaño del label
                    Location = new Point(10, 10), // Posición dentro del panel,ESTO LO DEBE DE TENER EL OTRO PANEL
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular)
                };

                // Crear el segundo label para el valor
                Label labelValor = new Label
                {
                    Text = medio.Valor.ToString("C0"), // Formatear como moneda
                    AutoSize = true,
                    Location = new Point(200, 10), // Posición dentro del panel
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
            CierreCajaRepository repository = new CierreCajaRepository();
            FrmMenuda frm = new FrmMenuda(ppal);

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

            bool actualizacionExitosa = new CierreCajaRepository().ActualizarCierre(IdCierre);
            if (!actualizacionExitosa)
            {
                MessageBox.Show("Hubo un error actualizando el cierre de caja");
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
                                    Valor = dr["Valor"].ToString()
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
                    Margin = new Padding(1) // Margen alrededor del panel
                };



                // Crear un contenedor para las descripciones y valores
                FlowLayoutPanel innerPanel = new FlowLayoutPanel
                {

                    FlowDirection = FlowDirection.TopDown,
                    Dock = DockStyle.Top,
                    AutoSize = true,
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

                        AutoSize = true
                    };


                    Label labelValor = new Label
                    {
                        Text = movimiento.Valor,
                        Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                        Width = 100, // Ancho del panel
                        ForeColor = Color.White,
                        //Margin = new Padding(0, 0, 10, 0),
                        TextAlign = ContentAlignment.MiddleRight
                    };

                    Label labelDescripcion = new Label
                    {
                        Text = movimiento.Descripcion,
                        Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                        ForeColor = Color.White,
                        Width = 180, // Ancho del panel
                        //Margin = new Padding(0, 0, 10, 0)

                    };


                    valuePanel.Controls.Add(labelDescripcion);
                    valuePanel.Controls.Add(labelValor);


                    innerPanel.Controls.Add(valuePanel); // Añadir el panel de valor-descripción al contenedor


                }

                panel.Controls.Add(innerPanel);
                panelesMovimientos.Controls.Add(panel);
            }
        }

        private void FrmDetalleReporte_Load(object sender, EventArgs e)
        {
            CargarSumatorias();
            CargarCierreVentas();
            ActualizarCiereCaja();
            CitarPanelesMovimientos();

        }
    }
}
