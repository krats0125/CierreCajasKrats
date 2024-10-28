using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
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
using TheArtOfDevHtmlRenderer.Adapters;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmReportes : Form
    {
        CONEXION cn = new CONEXION();
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            listarCierresCaja();
        }
        public void listarCierresCaja()
        {
            string sql = @"select IdCierre,IdUsuario AS NOMBRE,FechaApertura AS 'FECHA DE APERTURA',FechaCierre AS 'FECHA DE CIERRE',TotalMovimientosCaja AS 'MOVIMIENTOS DE CAJA',EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',TotalEfectivo 'EFECTIVO', TotalDatafono AS 'DATAFONOS',TotalTransferencia AS 'TRANSFERENCIAS',ValorVentas AS 'VENTAS',Diferencia AS 'DIFERENCIA',TotalLiquidado AS 'TOTAL LOQUIDADO'
                         from CierreCaja";
            DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
            dgvReporte.DataSource = lista;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string sql = "select * from CierreCaja where IdUsuario like '" + txtNombre.Text + "%%'";
                DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
                dgvReporte.DataSource = lista;
            }

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
    

        string sql = "SELECT * FROM CierreCaja WHERE CAST(FechaApertura AS DATE) = @Fecha";

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.Date);

                        conexion.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable lista = new DataTable();
                        da.Fill(lista);

                        dgvReporte.DataSource = lista;
                }
}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }



        }

        private void dgvReporte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                var fila = dgvReporte.Rows[e.RowIndex];


                var idCierre = Convert.ToInt32(fila.Cells["IdCierre"].Value);
                var idUsuario =fila.Cells["NOMBRE"].Value.ToString();

                // Abre el formulario de detalles
                FrmDetalleReporte frmDetalle = new FrmDetalleReporte(idCierre,idUsuario);
                frmDetalle.ShowDialog();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                // Podrías agregar más lógica aquí si es necesario
            }
        }
    }
}
