using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CierreDeCajas.Logica.Utilitarios
{
    // 1) ejecutar una sentencia sql (devuelve bool)
    // 2) Pasar la informacion a un Datatable (devuelve datatable)
    // 3) ejecutar sentencia y devolver un numero, (usado en el cierre de cajas)
    // 4) 

    public class SentenciaSqlServer
    {
        
        public bool EjecutarSQL(string sentenciaSQL, string CadenaConexion)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand(sentenciaSQL, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
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

        public DataTable TraerDatos(string sentenciaSQL, string CadenaConexion)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(sentenciaSQL,conexion))
                    {
                       da.Fill(dt);

                    }
                    conexion.Close();
                }
            }
            catch (Exception)
            {

                return dt;
            }

            return dt;
        }

    }
}
