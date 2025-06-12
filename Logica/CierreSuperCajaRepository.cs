using CierreDeCajas.Modelo;
using CierreDeCajas.Presentacion;
using CierreDeCajas.Presentacion.Administrativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica.Utilitarios
{
    public class CierreSuperCajaRepository
    {
        CierreSuperCaja oCierreSuperCaja=new CierreSuperCaja();
        SistemaRepository sistema ;
        FrmSuperCaja supercaja=null;
        CONEXION Conexion = new CONEXION();
        string novedades = null;
        decimal EfectivoSistema = 0;
        decimal datafonoSistema = 0;

        public CierreSuperCajaRepository(FrmSuperCaja supercaja)
        {
            this.sistema = new SistemaRepository(supercaja);
            this.supercaja= supercaja;

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
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.idCierre);
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
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.idCierre);
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
                        cmd.Parameters.AddWithValue("@IdCierre", supercaja.idCierre);
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

        public bool ActualizarCierre(int IdCierre)
        {

            decimal entregaUltimoEfectivo = 0;
            EfectivoSistema=cargarEfectivoSistema();
            datafonoSistema = cargarDatafonoSistema();

            FrmMenudaSuperCaja frm=new InstanciasRepository().InstanciaFrmMenudaSuperCaja();

            if (frm != null)
            {
                entregaUltimoEfectivo = frm.valorentregado;
            }

            FrmSuperCaja frmSc = new InstanciasRepository().InstanciaFrmSuperCaja();

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

        public DataTable ObtenerCierre(string IdUsuario, DateTime FechaApertura)
        {
            DataTable dt = new DataTable();
            string fechaformateada = FechaApertura.ToString("yyyy-MM-dd");
            try
            {

                using (SqlConnection cn = new SqlConnection(Conexion.ConexionCierreCaja()))
                {
                    cn.Open();
                    string consulta = $@"SELECT IdUsuario AS NOMBRE, FechaApertura AS 'FECHA DE APERTURA',
                                     EntregaUltimoEfectivo AS 'ULTIMO EFECTIVO ENTREGADO',
						             TotalMovimientosCaja AS 'TOTAL MOVIMIENTOS DE CAJA',
						             TotalEfectivo AS 'TOTAL EFECTIVO',
						             TotalEfectivoSistema AS 'TOTAL EFECTIVO SISTEMA',
						             DiferenciaEfectivo AS 'DIFERENCIA EN EFECTIVO',
                                      TotalDatafono AS 'TOTAL DATAFONOS',
						              TotalDatafonoSistema AS 'TOTAL DATAFONOS SISTEMA',
						              DiferenciaDatafonos AS 'DIFERENCIA EN DATAFONOS', 
                                      TotalCobrado AS 'TOTAL COBRADO',  TotalLiquidado AS 'TOTAL LIQUIDADO',
						              Diferencia AS 'DIFERENCIA'
                                      FROM CierreSuperCaja WHERE IdUsuario= '{IdUsuario}'
                                      AND CAST(FechaApertura AS DATE) = '{fechaformateada}'
                                      AND IdCierre={supercaja.idCierre}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, cn))
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
