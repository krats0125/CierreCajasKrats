using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.CodeDom;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CierreDeCajas.Logica
{
    public class MovimientosRepository
    {
        CONEXION cn = new CONEXION();

        public bool Insertar(Movimiento oMovimiento)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"INSERT INTO MovimientoCaja (IdCaja, IdUsuario,IdCierre, IdConcepto, Valor, Descripcion, IdMedioPago, Fecha)
                               VALUES (@IdCaja, @IdUsuario,@IdCierre, @IdConcepto, @Valor, @Descripcion, @IdMedioPago, @Fecha)";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        
                        cmd.Parameters.AddWithValue("@IdCaja", oMovimiento.IdCaja);
                        cmd.Parameters.AddWithValue("@IdUsuario", oMovimiento.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCierre", oMovimiento.IdCierre);
                        cmd.Parameters.AddWithValue("@IdConcepto", oMovimiento.IdConcepto);
                        cmd.Parameters.AddWithValue("@Valor", oMovimiento.Valor);
                        cmd.Parameters.AddWithValue("@Descripcion", oMovimiento.Descripcion);
                        cmd.Parameters.AddWithValue("@IdMedioPago", oMovimiento.IdMedioPago);
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

        public bool Editar(Movimiento oMovimiento)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion=new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"update MovimientoCaja
                    set 
                    IdConcepto=@IdConcepto,
                    Valor=@Valor,
                    Descripcion=@Descripcion,
                    IdMedioPago=@IdMedioPago
                    where IdMovimiento=@IdMovimiento";
                    using (SqlCommand cmd=new SqlCommand(sql,conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMovimiento", oMovimiento.IdMovimiento);
                        cmd.Parameters.AddWithValue("@IdConcepto",oMovimiento.IdConcepto);
                        cmd.Parameters.AddWithValue("@Valor", oMovimiento.Valor);
                        cmd.Parameters.AddWithValue("@Descripcion", oMovimiento.Descripcion);
                        cmd.Parameters.AddWithValue("@IdMedioPago", oMovimiento.IdMedioPago);

                        cmd.ExecuteNonQuery();
                        respuesta=true;
                    }
                }

            }
            catch (Exception)
            {
                return respuesta;
            }
         return respuesta ;
        
        }

        public bool Eliminar(Movimiento oMovimiento)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion=new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string consulta = @"delete from MovimientoCaja
                    where IdMovimiento=@IdMovimiento";

                    using (SqlCommand cmd=new SqlCommand(consulta,conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMovimiento",oMovimiento.IdMovimiento);
                        cmd.ExecuteNonQuery ();
                        respuesta=true;
                    }
                }
            }
            catch (Exception ex)
            {
                return respuesta;
            }
            return respuesta;
        }
        //public bool InsertarTemporal(Movimiento oMovimiento)
        //{
        //    bool respuesta = false;
        //    try
        //    {
        //        using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
        //        {
        //            conexion.Open();
        //            string sql = @"INSERT INTO TempMovimientoCaja (IdCaja, IdCajero, IdConcepto, Valor, Descripcion, IdMedioPago, Fecha)
        //                       VALUES (@IdCaja, @IdCajero, @IdConcepto, @Valor, @Descripcion, @IdMedioPago, @Fecha)";

        //            using (SqlCommand cmd = new SqlCommand(sql, conexion))
        //            {
        //                cmd.Parameters.AddWithValue("@IdCaja", oMovimiento.IdCaja);
        //                cmd.Parameters.AddWithValue("@IdCajero", oMovimiento.IdCajero);
        //                cmd.Parameters.AddWithValue("@IdConcepto", oMovimiento.IdConcepto);
        //                cmd.Parameters.AddWithValue("@Valor", oMovimiento.Valor);
        //                cmd.Parameters.AddWithValue("@Descripcion", oMovimiento.Descripcion);
        //                cmd.Parameters.AddWithValue("@IdMedioPago", oMovimiento.IdMedioPago);
        //                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);


        //                cmd.ExecuteNonQuery();
        //                respuesta = true;
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return respuesta;
        //    }

        //    return respuesta;
        //}
        public bool TransferirATablaReal()
        {
            bool respuesta = false;

            
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();

                    string sql = $@"INSERT INTO MovimientoCaja (IdCierre, IdCaja, IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago, fecha)
                   SELECT @NuevoIdCierre, IdCaja, IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago, Fecha
                   FROM TempMovimientoCaja
                   WHERE IdUsuario = @IdUsuario AND IdCaja = @IdCaja;";



                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {

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

        /*
         < alt + 60
         > alt + 62

         [ alt 91
         ] alt 93

        { alt 123
        } alt 125
         */
    }
}
