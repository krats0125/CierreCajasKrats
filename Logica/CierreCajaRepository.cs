using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;

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
        CierreCaja oCierreCaja = new CierreCaja();
        CONEXION cn = new CONEXION();
        //(pendiente)se modifico el valor de las ventas
        decimal valorVentas = 9371470;

        public decimal ActualizarVentas(string IdUsuario)
        {

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionRibisoft()))
                {
                    conexion.Open();
                    string sql = "select sum(total) from Facturas1 where IdUsuario=@IdUsuario AND CAST(FechaCreacion AS DATE) = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                valorVentas = Convert.ToDecimal(dr["sum(total)"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return oCierreCaja.ValorVentas;
            }
            return oCierreCaja.ValorVentas;
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
    }
 
}
