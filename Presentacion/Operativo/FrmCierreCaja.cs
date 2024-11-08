using CierreDeCajas.Logica;
using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Operativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CierreDeCajas.Presentacion
{
    public partial class FrmCierreCaja : Form
    {
        Principal ppal;
        CONEXION cn=new CONEXION();
 

        public FrmCierreCaja(Principal ppal)
        {
            InitializeComponent();
            this.ppal = ppal;
        


        }
        private void FrmVentas_TotalVentasChanged(decimal totalVentas)
        {
          
        }
       


        private void CierreCaja_Load(object sender, EventArgs e)
        {
            CargarSumatorias();
            cargarVentas();
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
                        using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                        {
                            conexion.Open();

                            using (SqlCommand cmd = new SqlCommand("usp_listarSumariaValoresPorCierreCajero", conexion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@IdUsuario", ppal.idUsuario));
                                cmd.Parameters.Add(new SqlParameter("@IdCierre", ppal.idCierre));

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
                if(lista.Count > 0 )
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
                CierreCajaRepository repository = new CierreCajaRepository(ppal);


                CierreCaja oCierreCaja =repository.listar(ppal.idCierre,out mensaje);

                if (oCierreCaja != null)
                {
                    lb_TotalEfectivo.Text = oCierreCaja.TotalEfectivo.ToString("C0");
                    lb_Diferencia.Text = oCierreCaja.Diferencia.ToString("C0");
                    lb_TotalDatafono.Text = oCierreCaja.TotalDatafono.ToString("C0");
                    lb_TotalLiquidado.Text = oCierreCaja.TotalLiquidado.ToString("C0");
                    lb_TotalMovimientosCaja.Text = oCierreCaja.TotalMovimientosCaja.ToString("C0");
                    lb_TotalTransferencia.Text = oCierreCaja.TotalTransferencia.ToString("C0");
                    lb_ValorVentas.Text = oCierreCaja.ValorVentas.ToString("C0");
                    lb_entregaultimoefectivo.Text = oCierreCaja.EntregaUltimoEfectivo.ToString("C0");

                }
            }



            public void ActualizarCiereCaja()
            {

                bool actualizacionExitosa = new CierreCajaRepository(ppal).ActualizarCierre(ppal.idCierre);
                if (!actualizacionExitosa)
                {
                    MessageBox.Show("Hubo un error actualizando el cierre de caja");
                }
                else
                {
                    CargarCierreVentas();
                }

            }
            private void lb_ValorVentas_Click(object sender, EventArgs e)
            {
           
            }
            private void cargarVentas()
            {
                CierreCajaRepository cierreCajaRepo = new CierreCajaRepository(ppal);

                decimal totalVentas = cierreCajaRepo.ActualizarVentas(ppal.idUsuario);


                if (totalVentas > 0)
                {
                    lb_ValorVentas.Text = totalVentas.ToString("C0");


                    bool actualizacionExitosa = cierreCajaRepo.ActualizarCierre(ppal.idCierre);

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
            where MC.IdUsuario='{ppal.idUsuario}' and MC.IdCierre= {ppal.idCierre}
			ORDER BY CM.Concepto";

            List<MovimientoList> movimientos = new List<MovimientoList>();

            CONEXION cn = new CONEXION();
            using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand(consulta,conexion))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
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

     
        private void PrintPage(PrintPageEventArgs e, DataTable datosCierre)
        {
            // Ajustes de impresión
            float yPos = 20;
            int leftMargin = 5;
            int bottomMargin = 20; // Margen inferior adicional

            using (Font font = new Font("Arial", 10))
            {
                foreach (DataRow row in datosCierre.Rows)
                {
                    e.Graphics.DrawString($"Nombre: {row["NOMBRE"]}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Fecha de Apertura: {row["FECHA DE APERTURA"]}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    yPos += 20;
                    e.Graphics.DrawString($"Movimientos de Caja: {string.Format("{0:N0}", row["TOTAL MOVIMIENTOS DE CAJA"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Último Efectivo Entregado: {string.Format("{0:N0}", row["ULTIMO EFECTIVO ENTREGADO"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Total Efectivo: {string.Format("{0:N0}", row["TOTAL EFECTIVO"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Datafonos: {string.Format("{0:N0}", row["TOTAL DATAFONOS"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Transferencias: {string.Format("{0:N0}", row["TOTAL TRANSFERENCIAS"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Ventas: {string.Format("{0:N0}", row["VENTAS"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Diferencia: {string.Format("{0:N0}", row["DIFERENCIA"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    e.Graphics.DrawString($"Total Liquidado: {string.Format("{0:N0}", row["TOTAL LIQUIDADO"])}", font, Brushes.Black, leftMargin, yPos);
                    yPos += 40; // Espacio adicional entre tirillas
                }
                yPos += bottomMargin;
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            CierreCajaRepository cierreCajaRepository = new CierreCajaRepository(ppal);
            DataTable datosCierre = cierreCajaRepository.ObtenerCierre(ppal.idUsuario, ppal.Fecha);

            if (datosCierre.Rows.Count > 0)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (s, ev) => PrintPage(ev, datosCierre);
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = pd
                };
                printPreviewDialog.ShowDialog();

                //pd.Print();
            }
        }
        private void cargarNovedades()
        {
            CierreCajaRepository cierrecaja = new CierreCajaRepository(ppal);
            string novedades = cierrecaja.CargarNovedades();
            if (novedades != null)
            {
                lbNovedades.Visible = true;
                txtnotas.Visible = true;
                txtnotas.Text = novedades;
            }

        }


        private void btnNotas_Click(object sender, EventArgs e)
        {
           
            FrmNotas notas = new FrmNotas(ppal);

            
            if (notas.ShowDialog() == DialogResult.OK)
            {
                
                txtnotas.Text = notas.nota; 
                lbNovedades.Visible = true;
                txtnotas.Visible = true; 

                if(txtnotas != null)
                {
                    lbNovedades.Visible = true;
                    txtnotas.Visible = true;
                }
            }
        }
    }
}


