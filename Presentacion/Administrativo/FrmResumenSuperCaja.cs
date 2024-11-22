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
    public partial class FrmResumenSuperCaja : Form
    {
        FrmSuperCaja supercaja;
        CONEXION Conexion=new CONEXION();
        public FrmResumenSuperCaja(FrmSuperCaja supercaja)
        {
            InitializeComponent();
            this.supercaja = supercaja;
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

                    using (SqlCommand cmd = new SqlCommand("usp_listarSumariaValoresPorCierreCajero", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario", supercaja.idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@IdCierre", supercaja.idCierre));

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
    }
}
