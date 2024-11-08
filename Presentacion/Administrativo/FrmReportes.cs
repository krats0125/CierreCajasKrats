using CierreDeCajas.Logica;
using CierreDeCajas.Logica.Utilitarios;
using CierreDeCajas.Modelo;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace CierreDeCajas.Presentacion.Administrativo
{
    public partial class FrmReportes : Form
    {
        CONEXION cn = new CONEXION();
        private int idCierre;
        private string idUsuario;
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
            dgvReporte.Refresh();
            dgvReporte.Columns[0].Visible = false;
            
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string sql = @"select IdCierre,IdUsuario AS NOMBRE,FechaApertura AS 'FECHA DE APERTURA',FechaCierre AS 'FECHA DE CIERRE',TotalMovimientosCaja AS 'MOVIMIENTOS DE CAJA',EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',TotalEfectivo 'EFECTIVO', TotalDatafono AS 'DATAFONOS',TotalTransferencia AS 'TRANSFERENCIAS',ValorVentas AS 'VENTAS',Diferencia AS 'DIFERENCIA',TotalLiquidado AS 'TOTAL LOQUIDADO' 
                             from CierreCaja where IdUsuario like '" + txtNombre.Text + "%%'";
                DataTable lista = new SentenciaSqlServer().TraerDatos(sql, cn.ConexionCierreCaja());
                dgvReporte.DataSource = lista;
            }

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {


            string sql = @"select IdCierre,IdUsuario AS NOMBRE,FechaApertura AS 'FECHA DE APERTURA',FechaCierre AS 'FECHA DE CIERRE',TotalMovimientosCaja AS 'MOVIMIENTOS DE CAJA',EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',TotalEfectivo 'EFECTIVO', TotalDatafono AS 'DATAFONOS',TotalTransferencia AS 'TRANSFERENCIAS',ValorVentas AS 'VENTAS',Diferencia AS 'DIFERENCIA',TotalLiquidado AS 'TOTAL LOQUIDADO'
                           FROM CierreCaja WHERE CAST(FechaApertura AS DATE) = @Fecha";

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
                    var idUsuario = fila.Cells["NOMBRE"].Value.ToString();
                    var fechaApertura = Convert.ToDateTime(fila.Cells["FECHA DE APERTURA"].Value);
                    // Abre el formulario de detalles
                    FrmDetalleReporte frmDetalle = new FrmDetalleReporte(idCierre, idUsuario, fechaApertura);
                    frmDetalle.ShowDialog();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                // Podrías agregar más lógica aquí si es necesario
            }
        }

        private void btnFinalizarCierre_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si hay una fila seleccionada
                if (dgvReporte.SelectedRows.Count > 0)
                {
                    var fila = dgvReporte.SelectedRows[0];
                    idCierre = Convert.ToInt32(fila.Cells["IdCierre"].Value);
                    idUsuario = fila.Cells["NOMBRE"].Value.ToString();

                    // Llama a la función para finalizar el cierre
                    bool resultado =finalizarcierre();

                    if (resultado==true)
                    {
                        listarCierresCaja();
                        Exportar();
                        MessageBox.Show("Cierre finalizado correctamente.");
                       
                    }
                    else
                    {
                        MessageBox.Show("Error al finalizar el cierre.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona una fila.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }

        }
        public bool finalizarcierre()
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"UPDATE CierreCaja
                                 SET FechaCierre=@FechaCierre
                                 where IdCierre=@IdCierre ";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {

                        cmd.Parameters.AddWithValue("@FechaCierre", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IdCierre", idCierre);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }


                }

            }
            catch (Exception ex)
            {
                return respuesta;
            }

            return respuesta;
        }

        private void Exportar()
        {

            using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
            {
                string consulta = @"
                                SELECT 
                               	mp.Descripcion AS Descripcion,
                               	SUM(mc.Valor) AS Valor,
                               	CONVERT(varchar, mc.Fecha, 103) AS Fecha
                               FROM 
                               	MovimientoCaja mc
                               	INNER JOIN MediosDePago mp ON mc.IdMedioPago = mp.IdMedioPago
                               	INNER JOIN CierreCaja CC ON CC.IdCierre = mc.idcierre
                               WHERE 
                               	CC.IdUsuario = @IdUsuario and cc.IdCierre = @IdCierre and mp.Descripcion<> 'Efectivo'
                               GROUP BY 
                                   cc.IdUsuario, mp.Descripcion, mc.Fecha
                               ORDER BY 
                                   mp.Descripcion";

                using (SqlCommand command = new SqlCommand(consulta, conexion))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdCierre", idCierre);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

             

                    // Crear el archivo Excel
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        // Configurar el diálogo
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Guardar archivo Excel";
                        saveFileDialog.FileName = $"CUADRE_{idUsuario}.xlsx"; 

                        // Mostrar el diálogo y esperar la respuesta del usuario
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Crear el archivo Excel
                            using (ExcelPackage excelPackage = new ExcelPackage())
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Resultados");

                                // Escribir el IdUsuario en la primera fila
                                worksheet.Cells["A1"].Value = $"CAJERO: {idUsuario}"; // Mostrar el IdUsuario

                                // Cargar los datos en la hoja de Excel a partir de la segunda fila
                                worksheet.Cells["A2"].LoadFromDataTable(dataTable, true);
                                worksheet.Cells.AutoFitColumns();


                                // Guardar el archivo en la ubicación seleccionada
                                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                                excelPackage.SaveAs(fileInfo);

                                // Mensaje de confirmación
                                MessageBox.Show($"Archivo guardado exitosamente", "Exportación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void pbFinalizarCierre_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si hay una fila seleccionada
                if (dgvReporte.SelectedRows.Count > 0)
                {
                    var fila = dgvReporte.SelectedRows[0];
                    idCierre = Convert.ToInt32(fila.Cells["IdCierre"].Value);
                    idUsuario = fila.Cells["NOMBRE"].Value.ToString();

                    // Llama a la función para finalizar el cierre
                    bool resultado = finalizarcierre();

                    if (resultado == true)
                    {
                        listarCierresCaja();
                        Exportar();
                        MessageBox.Show("Cierre finalizado correctamente.");

                    }
                    else
                    {
                        MessageBox.Show("Error al finalizar el cierre.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona una fila.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnGenerarInforme_Click(object sender, EventArgs e)
        {
            FrmGenerarInforme generarInforme = new FrmGenerarInforme();
            generarInforme.Show();
        }

        private void pbGenerarInforme_Click(object sender, EventArgs e)
        {
            FrmGenerarInforme generarInforme = new FrmGenerarInforme();
            generarInforme.Show();
        }
    }  
}

