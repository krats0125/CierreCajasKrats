using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class PrestamosRepository
    {
        CONEXION cn = new CONEXION();


        public int Insertar(Movimiento oMovimiento)
        {
            int idMovimiento = 0;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"INSERT INTO MovimientoCaja (IdCaja, IdUsuario,IdCierre, IdConcepto, Valor, Descripcion, IdMedioPago, Fecha)
                                VALUES (@IdCaja, @IdUsuario,@IdCierre, @IdConcepto, @Valor, @Descripcion, @IdMedioPago, @Fecha);
                                SELECT CAST(SCOPE_IDENTITY() AS int);";

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


                        idMovimiento = (int)cmd.ExecuteScalar();
                    }


                }

            }
            catch (Exception ex)
            {
                return 0;
            }

            return idMovimiento;
        }

        //public bool InsertarEnPrestamos(Prestamo oPrestamo, bool esMensajero,bool esTrabajador)
        //{
        //    bool respuesta = false;
        //    try
        //    {
        //        using (OleDbConnection conexion = new OleDbConnection(cn.ConexionDbInterna()))
        //        {
        //            conexion.Open();
        //            string sql = "";


        //            if (esMensajero)
        //            {
        //                sql = @"INSERT INTO PRESTAMOS_MENSAJEROS(IdTrabajador, Valor, Concepto, Caja, Cajero, Observacion,IdMovimiento)  
        //                VALUES (@IdTrabajador, @Valor, @Concepto, @Caja, @Cajero, @Observacion,@IdMovimiento)";
        //            }
        //            else if (esTrabajador)
        //            {
        //                sql = @"INSERT INTO PRESTAMOS(IdTrabajador, Valor, Concepto, Caja, Cajero, Observacion,IdMovimiento)  
        //                VALUES ( @IdTrabajador, @Valor, @Concepto, @Caja, @Cajero, @Observacion,@IdMovimiento)";
        //            }

        //            using (OleDbCommand cmd = new OleDbCommand(sql, conexion))
        //            {
        //                string idTrabajador = esMensajero ? oPrestamo.IdMensajero : oPrestamo.IdTrabajador;
        //                cmd.Parameters.AddWithValue("@IdTrabajador", idTrabajador);
        //                cmd.Parameters.AddWithValue("@Valor", oPrestamo.Valor);
        //                cmd.Parameters.AddWithValue("@Concepto", oPrestamo.Concepto);
        //                cmd.Parameters.AddWithValue("@Caja", oPrestamo.Caja);
        //                cmd.Parameters.AddWithValue("@Cajero", oPrestamo.Cajero);
        //                cmd.Parameters.AddWithValue("@Observacion", oPrestamo.Observacion);
        //                cmd.Parameters.AddWithValue("@IdMovimiento", oPrestamo.IdMovimiento);

        //                if (esMensajero)
        //                {
        //                    cmd.Parameters.AddWithValue("@IdMensajero", oPrestamo.IdMensajero);

        //                }
        //                else if (esTrabajador)
        //                {
        //                    cmd.Parameters.AddWithValue("@IdTrabajador", oPrestamo.IdTrabajador);

        //                }



        //                cmd.ExecuteNonQuery();
        //                respuesta = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al guardar el préstamo: {ex.Message}");
        //        return false;
        //    }

        //    return respuesta;


        //}


        public bool InsertarEnPrestamos(Prestamo oPrestamo, bool esMensajero, bool esTrabajador)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.Conexionlabodegadenacho()))
                {
                    conexion.Open();
                    string sql = "";


                    if (esMensajero)
                    {
                        sql = @"insert into PRESTAMOS_MENSAJEROS(Fecha,IdTrabajador,Valor,Concepto,Caja,Cajero,Observacion,IdMovimiento)
                              values(@Fecha,@IdMensajero, @Valor, @Concepto, @Caja, @Cajero, @Observacion,@IdMovimiento)";
                    }
                    else if (esTrabajador)
                    {
                        sql = @"insert into PRESTAMOS(Fecha,IdTrabajador,Valor,Concepto,Caja,Cajero,Observacion,IdMovimiento)
                               values(@Fecha,@IdTrabajador, @Valor, @Concepto, @Caja, @Cajero, @Observacion,@IdMovimiento)";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        string idTrabajador = esMensajero ? oPrestamo.IdMensajero : oPrestamo.IdTrabajador;
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Valor", oPrestamo.Valor);
                        cmd.Parameters.AddWithValue("@Concepto", oPrestamo.Concepto);
                        cmd.Parameters.AddWithValue("@Caja", oPrestamo.Caja);
                        cmd.Parameters.AddWithValue("@Cajero", oPrestamo.Cajero);
                        cmd.Parameters.AddWithValue("@Observacion", oPrestamo.Observacion);
                        cmd.Parameters.AddWithValue("@IdMovimiento", oPrestamo.IdMovimiento);

                        if (esMensajero)
                        {
                            cmd.Parameters.AddWithValue("@IdMensajero", oPrestamo.IdMensajero);

                        }
                        else if (esTrabajador)
                        {
                            cmd.Parameters.AddWithValue("@IdTrabajador", oPrestamo.IdTrabajador);

                        }



                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el préstamo: {ex.Message}");
                return false;
            }

            return respuesta;


        }
        public bool EliminarPrestamoTrabajador(Prestamo oPrestamo)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.Conexionlabodegadenacho()))
                {
                    conexion.Open();
                    string sql = @"DELETE FROM PRESTAMOS WHERE IdPrestamo = @idPrestamo";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPrestamo", oPrestamo.IdPrestamo);
                        cmd.ExecuteNonQuery();
                    }
                    respuesta = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el préstamo: {ex.Message}");
                return respuesta;
            }
            return respuesta;
        }

        public bool EliminarPrestamoMensajero(Prestamo oPrestamo)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.Conexionlabodegadenacho()))
                {
                    conexion.Open();

                    string sql = @"DELETE FROM PRESTAMOS_MENSAJEROS WHERE IdPrestamo = @idPrestamo";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPrestamo", oPrestamo.IdPrestamo);
                        cmd.ExecuteNonQuery();

                    }
                    respuesta = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el préstamo: {ex.Message}");
                return respuesta;
            }
            return respuesta;
        }

        public bool EliminarMovimientoCaja(int idMovimiento)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"delete from MovimientoCaja where IdMovimiento=@IdMovimiento";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMovimiento", idMovimiento);
                        cmd.ExecuteNonQuery();
                    }
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el movimiento de caja: {ex.Message}");
                return respuesta;
            }
            return respuesta;

        }
    }
}
