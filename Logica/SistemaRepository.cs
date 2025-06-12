using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class SistemaRepository
    {
        CONEXION cn = new CONEXION();

        FrmSuperCaja supercaja = null;
        public decimal TotalEfectivoSistema;
        public decimal TotalDatafonoSistema ;
        public SistemaRepository(FrmSuperCaja supercaja)
        {
            this.supercaja = supercaja;
        }
        public bool LeerYSumarValores(List<string> rutasArchivos)
        {
            CierreSuperCaja oCierresupercaja=new CierreSuperCaja();
            //List<Sistema> sistema = new List<Sistema>();

            try
            {
                foreach (string rutaArchivo in rutasArchivos)
                {
                    string nombreArchivo = Path.GetFileName(rutaArchivo).Trim();

                    using (var reader = new StreamReader(rutaArchivo))
                    {
                        string encabezados = reader.ReadLine(); // Leer la primera línea (encabezados)

                        while (!reader.EndOfStream)
                        {
                            var linea = reader.ReadLine();
                            var valores = linea.Split(',');

                            // Variables para las columnas específicas
                            decimal valorEfectivo = 0, valorTarjeta = 0, valorNeto = 0,valorDevolucion=0;

                            if (nombreArchivo.StartsWith("pedidosDetallado"))
                            {
                                valorEfectivo = Convert.ToDecimal(valores[6]);
                                valorTarjeta = Convert.ToDecimal(valores[7]);
                                valorDevolucion=Convert.ToDecimal(valores[9]);
                                //valorNeto = Convert.ToDecimal(valores[10]);

                            }

                 
                            if (valorEfectivo > 0)
                            {

                                TotalEfectivoSistema += valorEfectivo - valorDevolucion;
                                //sistema.Add(new Sistema {Valor = valorEfectivo - valorDevolucion, MedioDePago = 1 });
                            }
                           if (valorTarjeta > 0)
                            {

                                TotalDatafonoSistema += valorTarjeta - valorDevolucion;
                                //sistema.Add(new Sistema { Valor = valorTarjeta - valorDevolucion, MedioDePago = 3 });
                            }
                        }
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar archivos CSV: " + ex.Message);
                return false;
            }
        }

  

        public bool InsertarEnBaseDeDatos(CierreSuperCaja oCierresupercaja)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Se eliminó el archivo anterior. ¿Desea continuar?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                    {
                        conexion.Open();

                        // 1. Intentar obtener el IdCierre con FechaCierre NULL para ese usuario
                        string consultaExistente = @"
                                                 SELECT IdCierre 
                                                 FROM CierreSuperCaja 
                                                 WHERE IdUsuario = @IdUsuario 
                                                 AND FechaCierre IS NULL 
                                                 AND CONVERT(date, FechaApertura) =CONVERT(DATE, DATEADD(HOUR, -5, GETDATE()))";

                        int idCierreExistente = 0;

                        using (SqlCommand cmdExistente = new SqlCommand(consultaExistente, conexion))
                        {
                            cmdExistente.Parameters.AddWithValue("@IdUsuario", supercaja.idUsuario);
                            var result = cmdExistente.ExecuteScalar();
                            if (result != null)
                            {
                                idCierreExistente = Convert.ToInt32(result);
                            }
                        }

                        if (idCierreExistente > 0)
                        {
                            // Eliminar los valores anteriores
                            string consultaEliminarValores = @" UPDATE CierreSuperCaja 
                                                             SET TotalEfectivoSistema = 0, TotalDatafonoSistema = 0
                                                             WHERE IdCierre = @IdCierre";

                            using (SqlCommand cmdEliminar = new SqlCommand(consultaEliminarValores, conexion))
                            {
                                cmdEliminar.Parameters.AddWithValue("@IdCierre", idCierreExistente);
                                cmdEliminar.ExecuteNonQuery();
                            }

                            // Ahora insertamos los nuevos valores
                            string consultaInsertar = @"
                                                      UPDATE CierreSuperCaja 
                                                      SET TotalEfectivoSistema = @TotalEfectivoSistema, 
                                                          TotalDatafonoSistema = @TotalDatafonoSistema
                                                      WHERE IdCierre = @IdCierre";

                            using (SqlCommand cmdInsert = new SqlCommand(consultaInsertar, conexion))
                            {
                                cmdInsert.Parameters.AddWithValue("@TotalEfectivoSistema", TotalEfectivoSistema);
                                cmdInsert.Parameters.AddWithValue("@TotalDatafonoSistema", TotalDatafonoSistema);
                                //cmdInsert.Parameters.AddWithValue("@TotalEfectivoSistema",TotalEfectivoSistema);
                                //cmdInsert.Parameters.AddWithValue("@TotalDatafonoSistema", TotalDatafonoSistema);
                                cmdInsert.Parameters.AddWithValue("@IdCierre", idCierreExistente);

                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Si no existe un cierre abierto, lanzamos un error o un mensaje
                            MessageBox.Show("No se puede insertar valores porque no existe un cierre abierto. Por favor, cierre el cierre anterior.");
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar en la base de datos: " + ex.Message);
                return false;
            }
        }



    }
}
