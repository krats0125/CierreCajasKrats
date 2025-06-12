using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica
{
    public class MovimientoSuperCajaRepository
    {
        CONEXION cn = new CONEXION();

        public bool Insertar(MovimientoSuperCaja oMovimientoSc)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"INSERT INTO MovimientoSuperCaja ( IdUsuario,IdCierre, IdConcepto, Valor, Descripcion, IdMedioPago, Fecha)
                               VALUES ( @IdUsuario,@IdCierre, @IdConcepto, @Valor, @Descripcion, @IdMedioPago, @Fecha)";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {

                        cmd.Parameters.AddWithValue("@IdUsuario", oMovimientoSc.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCierre", oMovimientoSc.IdCierre);
                        cmd.Parameters.AddWithValue("@IdConcepto", oMovimientoSc.IdConcepto);
                        cmd.Parameters.AddWithValue("@Valor", oMovimientoSc.Valor);
                        cmd.Parameters.AddWithValue("@Descripcion", oMovimientoSc.Descripcion);
                        cmd.Parameters.AddWithValue("@IdMedioPago", oMovimientoSc.IdMedioPago);
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);


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

        public bool Editar(MovimientoSuperCaja oMovimientoSc)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"update MovimientoSuperCaja
                    set 
                    IdConcepto=@IdConcepto,
                    Valor=@Valor,
                    Descripcion=@Descripcion,
                    IdMedioPago=@IdMedioPago
                    where IdMovimiento=@IdMovimiento";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMovimiento", oMovimientoSc.IdMovimiento);
                        cmd.Parameters.AddWithValue("@IdConcepto", oMovimientoSc.IdConcepto);
                        cmd.Parameters.AddWithValue("@Valor", oMovimientoSc.Valor);
                        cmd.Parameters.AddWithValue("@Descripcion", oMovimientoSc.Descripcion);
                        cmd.Parameters.AddWithValue("@IdMedioPago", oMovimientoSc.IdMedioPago);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }
                }

            }
            catch (Exception)
            {
                return respuesta;
            }
            return respuesta;

        }

        public bool Eliminar(MovimientoSuperCaja oMovimientoSc)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string consulta = @"delete from MovimientoSuperCaja
                    where IdMovimiento=@IdMovimiento";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMovimiento", oMovimientoSc.IdMovimiento);
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
    }
}
