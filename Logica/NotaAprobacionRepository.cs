using CierreDeCajas.Modelo;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica
{
    public class NotaAprobacionRepository
    {
        CONEXION cn = new CONEXION();
        string notaAprobacion = "";
        public bool InsertarNota(NotaAprobacion oNota)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"Insert into NotasAprobacion(idCierre, Descripcion)
                                   values(@IdCierre, @Descripcion)";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", oNota.IdCierre);
                        cmd.Parameters.AddWithValue("@Descripcion", oNota.Descripcion);
                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool ActualizarNota(NotaAprobacion oNota)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"Update NotasAprobacion set Descripcion=@Descripcion where idCierre=@idcierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idcierre", oNota.IdCierre);
                        cmd.Parameters.AddWithValue("@Descripcion", oNota.Descripcion);
                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        public string mostrarNota(int idcierre)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"select descripcion from NotasAprobacion where idCierre=@idcierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idcierre", idcierre);
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

        public int ObtenerNotaPorIdCierre(int idCierre)
        {
            int nota = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"select count(*) from NotasAprobacion where idCierre=@idCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdCierre", idCierre);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            nota = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
            return nota;
        }

        public void MarcarNotaComoMostrada(int idCierre)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"UPDATE NotasAprobacion SET NotaMostrada = 1 WHERE idCierre=@idCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idCierre", idCierre);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
        }

        public void RestablecerNotaMostrada(int idCierre)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"UPDATE NotasAprobacion SET NotaMostrada = 0 WHERE idCierre=@idCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idCierre", idCierre);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
        }

        public bool NotaMostrada(int idcierre)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"SELECT NotaMostrada FROM NotasAprobacion WHERE idCierre=@idCierre";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idCierre", idcierre);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            respuesta = Convert.ToBoolean(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
            return respuesta;

        }
    }
}
