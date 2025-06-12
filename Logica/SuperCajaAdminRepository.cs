using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion.Administrativo;
using CierreDeCajas.Presentacion.AdminSuperCaja;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CierreDeCajas.Logica.Utilitarios
{
    public class SuperCajaAdminRepository
    {
        CierreSuperCaja oCierreSuperCaja = new CierreSuperCaja();
        FrmResumenSuperCajaAdmin supercaja = null;
        CONEXION Conexion = new CONEXION();
        string novedades = null;
        public decimal EfectivoSistema = 0;
        public decimal datafonoSistema = 0;
        decimal entregaUltimoEfectivo = 0;

        public SuperCajaAdminRepository(FrmResumenSuperCajaAdmin supercaja)
        {
            //this.sistema = new SistemaRepository(supercaja);
            this.supercaja = supercaja;

        }

        public string CargarNovedades()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "select descripcion from NovedadesSuperCaja where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            novedades = Convert.ToString(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return novedades;
        }


        public decimal cargarEfectivoSistema()
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "select TotalEfectivoSistema from CierreSuperCaja where IdCierre=@IdCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            EfectivoSistema = Convert.ToDecimal(result);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return EfectivoSistema;
        }

        public decimal cargarDatafonoSistema()
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "select TotalDatafonoSistema from CierreSuperCaja where IdCierre=@IdCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            datafonoSistema = Convert.ToDecimal(result);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
            return datafonoSistema;
        }

        public decimal CargarValorUltimoEfectivo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = "SELECT EntregaUltimoEfectivo FROM CierreSuperCaja WHERE IdCierre = @IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.IdCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            entregaUltimoEfectivo = Convert.ToDecimal(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor del ultimo efectivo guardado: {ex.Message}");
            }
            return entregaUltimoEfectivo;
        }

        public bool ActualizarCierre(int IdCierre)
        {

          
            EfectivoSistema = cargarEfectivoSistema();
            datafonoSistema = cargarDatafonoSistema();
            entregaUltimoEfectivo = CargarValorUltimoEfectivo();

            FrmMenudaSuperCajaAdmin frm = new InstanciasRepository().InstanciaFrmMenudaSuperCajaAdmin();

            if (frm != null)
            {
                if(frm.valorentregado>0)
                {
                    entregaUltimoEfectivo = frm.valorentregado;
                }
               
            }

            FrmResumenSuperCajaAdmin frmSc = new InstanciasRepository().InstanciaFrmSuperCajaAdmin();

            if (frmSc != null)
            {
                // Solo actualizamos si los valores son mayores a cero.
                if (frmSc.totalEfectivoSistema > 0)
                {
                    EfectivoSistema = frmSc.totalEfectivoSistema;
                }

                if (frmSc.totalDatafonoSistema > 0)
                {
                    datafonoSistema = frmSc.totalDatafonoSistema;
                }
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ActualizarCierreSuperCaja", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);
                        cmd.Parameters.AddWithValue("@EntregaUltimoEfectivo", entregaUltimoEfectivo);
                        cmd.Parameters.AddWithValue("@TotalEfectivoSistema", EfectivoSistema);
                        cmd.Parameters.AddWithValue("@TotalDatafonoSistema", datafonoSistema);


                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return respuesta;
            }

            return respuesta;
        }


        public CierreSuperCaja listar(int IdCierre, out string mensaje)
        {
            mensaje = string.Empty;
            CierreSuperCaja oCierreSuperCaja = new CierreSuperCaja();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string sql = @"select *
                                   from CierreSuperCaja
                                   where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {

                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oCierreSuperCaja.TotalEfectivo = Convert.ToDecimal(dr["TotalEfectivo"].ToString());
                                oCierreSuperCaja.TotalEfectivoSistema = Convert.ToDecimal(dr["TotalEfectivoSistema"].ToString());
                                oCierreSuperCaja.DiferenciaEfectivo = Convert.ToDecimal(dr["DiferenciaEfectivo"].ToString());
                                oCierreSuperCaja.Diferencia = Convert.ToDecimal(dr["Diferencia"].ToString());
                                oCierreSuperCaja.TotalMovimientosCaja = Convert.ToDecimal(dr["TotalMovimientosCaja"].ToString());
                                oCierreSuperCaja.EntregaUltimoEfectivo = Convert.ToDecimal(dr["EntregaUltimoEfectivo"].ToString());
                                oCierreSuperCaja.TotalDatafono = Convert.ToDecimal(dr["TotalDatafono"].ToString());
                                oCierreSuperCaja.TotalDatafonoSistema = Convert.ToDecimal(dr["TotalDatafonoSistema"].ToString());
                                oCierreSuperCaja.DiferenciaDatafono = Convert.ToDecimal(dr["DiferenciaDatafonos"].ToString());
                                oCierreSuperCaja.TotalLiquidado = Convert.ToDecimal(dr["TotalLiquidado"].ToString());
                                oCierreSuperCaja.TotalCobrado = Convert.ToDecimal(dr["TotalCobrado"].ToString());

                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "hubo un error : " + ex.Message;
                return oCierreSuperCaja;
            }

            return oCierreSuperCaja;
        }

        public bool LeerYSumarValores(List<string> rutasArchivos)
        {
            CierreSuperCaja oCierresupercaja = new CierreSuperCaja();
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
                            decimal valorEfectivo = 0, valorTarjeta = 0, valorNeto = 0, valorDevolucion = 0;

                            if (nombreArchivo.StartsWith("pedidosDetallado"))
                            {
                                valorEfectivo = Convert.ToDecimal(valores[6]);
                                valorTarjeta = Convert.ToDecimal(valores[7]);
                                valorDevolucion = Convert.ToDecimal(valores[9]);
                                //valorNeto = Convert.ToDecimal(valores[10]);

                            }


                            if (valorEfectivo > 0)
                            {

                                EfectivoSistema += valorEfectivo - valorDevolucion;
                                //sistema.Add(new Sistema {Valor = valorEfectivo - valorDevolucion, MedioDePago = 1 });
                            }
                            if (valorTarjeta > 0)
                            {

                                datafonoSistema += valorTarjeta - valorDevolucion;
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



        public bool InsertarEnBaseDeDatos(CierreSuperCaja oCierresupercaja,DateTime fecha)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Se eliminó el archivo anterior. ¿Desea continuar?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conexion = new SqlConnection(Conexion.ConexionCierreCaja()))
                    {
                        conexion.Open();

                        // 1. Intentar obtener el IdCierre con FechaCierre NULL para ese usuario
                        string consultaExistente = @"
                                                  SELECT IdCierre 
                                                 FROM CierreSuperCaja 
                                                 WHERE IdUsuario = @IdUsuario 
                                                 AND FechaCierre IS NULL 
                                                 AND CONVERT(date, FechaApertura) =CONVERT(date,@fecha)";

                        int idCierreExistente = 0;

                        using (SqlCommand cmdExistente = new SqlCommand(consultaExistente, conexion))
                        {
                            cmdExistente.Parameters.AddWithValue("@IdUsuario", supercaja.IdUsuario);
                            cmdExistente.Parameters.AddWithValue("@fecha", fecha);
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
                                cmdInsert.Parameters.AddWithValue("@TotalEfectivoSistema", EfectivoSistema);
                                cmdInsert.Parameters.AddWithValue("@TotalDatafonoSistema", datafonoSistema);
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
