using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class CierreCajaRepository
    {
        CierreCaja oCierreCaja = new CierreCaja();
        CONEXION cn = new CONEXION();
        decimal valorVentas = 0;
        decimal valorDevoluciones=0;
        string novedades=null;
        string notaAprobacion = null;
        Principal ppal = null;


        bool test = false;
        const string mesDiaPrueba = "0702";

        public CierreCajaRepository(Principal principal)
        {
            ppal = principal;
            CargarValorVentas();
           

        }

        public decimal ActualizarVentas2(string IdUsuario)
        {
            decimal ventasNuevas = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionRibisoft()))
                {
                    conexion.Open();
                    string sql = $@"SELECT 
                             SUM(F1.total) AS VENTAS,
                             ISNULL(
                                 (SELECT SUM(NC.total) 
                                  FROM Notas_CxC1 NC 
                                  WHERE NC.IdUsuario = @IdUsuario
                                  AND CAST(NC.FechaCreacion AS DATE) = CAST(DATEADD(HOUR, -5, GETDATE()) AS DATE)), 0) AS DEVOLUCIONES,
                             SUM(F1.total) - 
                             ISNULL(
                                 (SELECT SUM(NC.total) 
                                  FROM Notas_CxC1 NC 
                                  WHERE NC.IdUsuario = @IdUsuario
                                  AND CAST(NC.FechaCreacion AS DATE) = CAST(DATEADD(HOUR, -5, GETDATE()) AS DATE)), 0) AS VentasTotales
                         FROM 
                             Facturas1 F1
                         WHERE 
                             F1.IdUsuario =@IdUsuario
							 AND F1.Anulado=0
                             AND CAST(F1.FechaCreacion AS DATE) = CAST(DATEADD(HOUR, -5, GETDATE()) AS DATE);";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ventasNuevas = Convert.ToDecimal(dr["VentasTotales"]);
                                if (ventasNuevas > 0)
                                {
                                    valorVentas = ventasNuevas;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return valorVentas;
        }
        public decimal ActualizarVentas(string IdUsuario)
        {
            decimal ventasNuevas = 0;

            
           

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;
            
            string tablaVentaMes = "Ventae" + mes;

            if (test)
            {
                mesDia = mesDiaPrueba;

            }


            try
            {
                using (OdbcConnection conexion = new OdbcConnection(cn.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string sql = $@"SELECT
                                    SUM(IIF(Estado  = 'PAGADA', total, 0)) AS VentasTotales
                                    FROM {tablaVentaMes} WHERE mmdd = '{mesDia}' and vendedor = '{IdUsuario}'";

                    sql = sql.Trim();

                    using (OdbcCommand cmd = new OdbcCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                        using (OdbcDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ventasNuevas = Convert.ToDecimal(dr["VentasTotales"]);
                                if (ventasNuevas > 0)
                                {
                                    valorVentas = ventasNuevas;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return valorVentas;
        }
        public void CargarValorVentas()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = "SELECT ValorVentas FROM CierreCaja WHERE IdCierre = @IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            valorVentas = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar valor de ventas guardado: {ex.Message}");
            }
        }


        public decimal ActualizarDevoluciones2(string idUsuario)
        {
            decimal devoluciones = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionRibisoft()))
                {
                    conexion.Open();
                    string consulta = @"select SUM(total) from Notas_CxC1 
                                    where IdUsuario=@IdUsuario AND CONVERT(date,FechaCreacion)=CONVERT(date,DATEADD(HOUR, -5, GETDATE()))";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);

                        var resultado = cmd.ExecuteScalar();

                        if (resultado != DBNull.Value)
                        {
                            devoluciones = Convert.ToDecimal(resultado);
                            if (devoluciones > 0)
                            {
                                valorDevoluciones = devoluciones;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return valorDevoluciones;
        }
        public decimal ActualizarDevoluciones(string idUsuario)
        {
            decimal devoluciones = 0;

            string mes = DateTime.Now.ToString("MM");
            string dia = DateTime.Now.ToString("dd");

            string mesDia = mes + dia;
            string tablaVentaMes = "Ventae" + mes;


            if (test)
            {
                mesDia = mesDiaPrueba;

            }

            try
            {
                using (OdbcConnection conexion = new OdbcConnection(cn.ConexionVisualFoxPro()))
                {
                    conexion.Open();
                    string consulta = $@"SELECT
                                         SUM(IIF(Estado  = 'ANULADA', total, 0)) AS Devoluciones
                                         FROM {tablaVentaMes} WHERE mmdd = '{mesDia}' and vendedor = '{idUsuario}'";

                    consulta = consulta.Trim();

                    using (OdbcCommand cmd = new OdbcCommand(consulta, conexion))
                    {
                        var resultado = cmd.ExecuteScalar();

                        if (resultado != DBNull.Value)
                        {
                            devoluciones = Convert.ToDecimal(resultado);
                            if (devoluciones > 0)
                            {
                                valorDevoluciones = devoluciones;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return valorDevoluciones;
        }


        public string CargarNovedades()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = "select descripcion from novedades where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
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
                Console.WriteLine($"Error al cargar las notas: {ex.Message}");
            }
            return novedades;
        }
        public string mostrarNota()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"select descripcion from NotasAprobacion where idCierre=@idcierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idcierre", ppal.idCierre);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            notaAprobacion = Convert.ToString(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notaAprobacion = "";
            }
            return notaAprobacion;
        }



        public bool ActualizarCierre(int IdCierre)
        {

            decimal entregaUltimoEfectivo = 0;
            
            FrmMenuda Frm = new InstanciasRepository().InstanciaFrmMenuda();
            
            if(Frm !=null)
            {
                entregaUltimoEfectivo=Frm.valorentregado;
            }

            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("usp_ActualizarCierreCaja", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);
                        cmd.Parameters.AddWithValue("@EntregaUltimoEfectivo", entregaUltimoEfectivo);
                        cmd.Parameters.AddWithValue("@ValorVentas", valorVentas);

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

       


        public CierreCaja listar(int IdCierre, out string mensaje)
        {
            mensaje = string.Empty;
            CierreCaja oCierreCaja = new CierreCaja();
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"select *
                                   from CierreCaja
                                   where IdCierre=@IdCierre";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                      
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);

                        using (SqlDataReader dr=cmd.ExecuteReader())
                        {
                            while (dr.Read()) 
                            {
                                oCierreCaja.TotalEfectivo =Convert.ToDecimal(dr["TotalEfectivo"].ToString());
                                oCierreCaja.ValorVentas = Convert.ToDecimal(dr["ValorVentas"].ToString());
                                oCierreCaja.Diferencia = Convert.ToDecimal(dr["Diferencia"].ToString());
                                oCierreCaja.TotalMovimientosCaja = Convert.ToDecimal(dr["TotalMovimientosCaja"].ToString());
                                oCierreCaja.EntregaUltimoEfectivo = Convert.ToDecimal(dr["EntregaUltimoEfectivo"].ToString());
                                oCierreCaja.TotalDatafono = Convert.ToDecimal(dr["TotalDatafono"].ToString());
                                oCierreCaja.TotalTransferencia = Convert.ToDecimal(dr["TotalTransferencia"].ToString());
                                oCierreCaja.TotalLiquidado = Convert.ToDecimal(dr["TotalLiquidado"].ToString());

                            }
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "hubo un error : " + ex.Message;
                return oCierreCaja;
            }

            return oCierreCaja;
        }

        public DataTable ObtenerCierre(string IdUsuario, DateTime FechaApertura)
        {
            DataTable dt = new DataTable();
            string fechaformateada = FechaApertura.ToString("yyyy-MM-dd");
            try
            {
                
                using (SqlConnection conexion=new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string consulta = $@"SELECT IdUsuario AS NOMBRE, FechaApertura AS 'FECHA DE APERTURA',
                         TotalMovimientosCaja AS 'TOTAL MOVIMIENTOS DE CAJA',
                         EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO', TotalEfectivo AS 'TOTAL EFECTIVO', 
                         TotalDatafono AS 'TOTAL DATAFONOS', TotalTransferencia AS 'TOTAL TRANSFERENCIAS', 
                         ValorVentas AS 'VENTAS', Diferencia AS 'DIFERENCIA', TotalLiquidado AS 'TOTAL LIQUIDADO'
                         FROM CierreCaja WHERE IdUsuario= '{IdUsuario}'
                         AND CAST(FechaApertura AS DATE) = '{fechaformateada}'
                         AND IdCierre={ppal.idCierre}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion))
                    {

                        adapter.Fill(dt);
                        return dt;
                    }
                       
                        
                    
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dt;
        }
    
    }

  
 
}
