using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Operativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class CierreCajaRepository
    {
        
        CONEXION cn = new CONEXION();
        public bool ActualizarCierre(int IdCierre,decimal valorventas)
        {
          
            decimal ultimoefectivoentregado = 0;
            FrmMenuda Frm = new InstanciasRepository().InstanciaFrmMenuda();
          
            if(Frm != null)
            {
                ultimoefectivoentregado = Frm.valorentregado;
            }
            if (valorventas == 0)
            {
                return true;
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
                        cmd.Parameters.AddWithValue("@EntregaUltimoEfectivo", ultimoefectivoentregado);
                        cmd.Parameters.AddWithValue("@ValorVentas", valorventas);

                

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

        public decimal ObtenerUltimoValorVentas(int IdCierre)
        {
            decimal valorVentas = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = "SELECT ValorVentas FROM CierreCaja WHERE IdCierre = @IdCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", IdCierre);
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            valorVentas = Convert.ToDecimal(resultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el valor de ventas: " + ex.Message);
            }
            return valorVentas;
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
    }
 
}
