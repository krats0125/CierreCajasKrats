using CierreDeCajas.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace CierreDeCajas.Logica
{
    public class TrabajadoresRepository
    {
        CONEXION cn=new CONEXION();
        public bool Insertar(Trabajador oTrabajador)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion =new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"insert into Usuario(IdUsuario,Nombre,IdRol,Activo)
                    values (@IdUsuario,@Nombre,@IdRol,@Activo)";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", oTrabajador.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", oTrabajador.Nombre);
                        cmd.Parameters.AddWithValue("@IdRol", oTrabajador.IdRol);
                        cmd.Parameters.AddWithValue("@Activo", oTrabajador.Activo);
             


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

        public bool Actualizar(Trabajador oTrabajador)
        {
            bool respuesta=false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open ();
                    string sql = @"update Usuario 
                    set Nombre=@Nombre,
                    IdRol=@IdRol,
	                Activo=@Activo
                    where IdUsuario=@IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", oTrabajador.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", oTrabajador.Nombre);
                        cmd.Parameters.AddWithValue("@IdRol", oTrabajador.IdRol);
                        cmd.Parameters.AddWithValue("@Activo",oTrabajador.Activo);

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

        public bool Eliminar(Trabajador oTrabajador)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                {
                    conexion.Open();
                    string sql = @"Delete from Usuario
                    where IdUsuario=@IdUsuario";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", oTrabajador.IdUsuario);
                        cmd.ExecuteNonQuery ();
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
