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

namespace CierreDeCajas.Presentacion.AdminSuperCaja
{
    public partial class FrmReporteSuperCaja : Form
    {
        CONEXION cn = new CONEXION();
        private int idCierre;
        private string idUsuario;
        public FrmReporteSuperCaja()
        {
            InitializeComponent();
        }

        private void FrmReporteSuperCaja_Load(object sender, EventArgs e)
        {
            listarCierresCaja();
        }
        public void listarCierresCaja()
        {
            string sql = @"SELECT 
                          IdCierre,
                          IdUsuario AS NOMBRE,
                          FechaApertura AS 'FECHA DE APERTURA',
                          FechaCierre AS 'FECHA DE CIERRE',
                          FORMAT(EntregaUltimoEfectivo, 'C0', 'es-CO') AS 'ULTIMO EFECTIVO ENTREGADO',
                          FORMAT(TotalMovimientosCaja, 'C0', 'es-CO') AS 'MOVIMIENTOS DE CAJA',
                          FORMAT(TotalEfectivo, 'C0', 'es-CO') AS 'EFECTIVO',
                          FORMAT(TotalEfectivoSistema, 'C0', 'es-CO') AS 'EFECTIVO SISTEMA',
                          FORMAT(DiferenciaEfectivo, 'C0', 'es-CO') AS 'DIFERENCIA EFECTIVO',
                          FORMAT(TotalDatafono, 'C0', 'es-CO') AS 'DATAFONOS',
                          FORMAT(TotalDatafonoSistema, 'C0', 'es-CO') AS 'DATAFONOS SISTEMA',
                          FORMAT(DiferenciaDatafonos, 'C0', 'es-CO') AS 'DIFERENCIA DATAFONOS',
                          FORMAT(TotalLiquidado, 'C0', 'es-CO') AS 'TOTAL LIQUIDADO',
                          FORMAT(TotalCobrado, 'C0', 'es-CO') AS 'COBRADO',
                          FORMAT(Diferencia, 'C0', 'es-CO') AS 'DIFERENCIA'
                      FROM 
                          CierreSuperCaja
                      ORDER BY 
                          [FECHA DE APERTURA] DESC";
            DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
            dgvReporte.DataSource = lista;
            dgvReporte.Refresh();
            dgvReporte.Columns[0].Visible = false;

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string sql = @"select IdCierre,IdUsuario AS NOMBRE,FechaApertura AS 'FECHA DE APERTURA',FechaCierre AS 'FECHA DE CIERRE',EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',TotalMovimientosCaja AS 'MOVIMIENTOS DE CAJA',TotalEfectivo 'EFECTIVO',TotalEfectivoSistema 'EFECTIVO SISTEMA',DiferenciaEfectivo 'DIFERENCIA EFECTIVO', TotalDatafono AS 'DATAFONOS',TotalDatafonoSistema AS 'DATAFONOS SISTEMA',DiferenciaDatafonos AS 'DIFERENCIA DATAFONOS',TotalLiquidado AS 'TOTAL LOQUIDADO',TotalCobrado AS 'COBRADO',Diferencia AS 'DIFERENCIA'
                             from CierreSuperCaja where IdUsuario like '" + txtNombre.Text + "%%'";
                DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
                dgvReporte.DataSource = lista;
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            string sql = @"select IdCierre,IdUsuario AS NOMBRE,FechaApertura AS 'FECHA DE APERTURA',FechaCierre AS 'FECHA DE CIERRE',EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',TotalMovimientosCaja AS 'MOVIMIENTOS DE CAJA',TotalEfectivo 'EFECTIVO',TotalEfectivoSistema 'EFECTIVO SISTEMA',DiferenciaEfectivo 'DIFERENCIA EFECTIVO', TotalDatafono AS 'DATAFONOS',TotalDatafonoSistema AS 'DATAFONOS SISTEMA',DiferenciaDatafonos AS 'DIFERENCIA DATAFONOS',TotalLiquidado AS 'TOTAL LOQUIDADO',TotalCobrado AS 'COBRADO',Diferencia AS 'DIFERENCIA'
                         from CierreSuperCaja WHERE CAST(FechaApertura AS DATE) = @Fecha";

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
                if(e.RowIndex>=0)
                {
                    var fila=dgvReporte.Rows[e.RowIndex];

                    var idCierre = Convert.ToInt32(fila.Cells["IdCierre"].Value);
                    var idUsuario = fila.Cells["NOMBRE"].Value.ToString();
                    var fechaApertura = Convert.ToDateTime(fila.Cells["FECHA DE APERTURA"].Value);


                    FrmResumenSuperCajaAdmin superCajaAdmin = new FrmResumenSuperCajaAdmin(idCierre,idUsuario,fechaApertura);
                    superCajaAdmin.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
