using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace CierreDeCajas.Logica.Utilitarios
{
    public class SentenciasSqlODBC
    {

        public bool EjecutarSQL(string sentenciaSQL, string CadenaConexion)
        {
            bool respuesta = false;

            try
            {
                using (OdbcConnection conexion = new OdbcConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (OdbcCommand cmd = new OdbcCommand(sentenciaSQL, conexion))
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
                using (OdbcConnection conexion = new OdbcConnection(CadenaConexion))
                {
                    conexion.Open();

                    using (OdbcDataAdapter da = new OdbcDataAdapter(sentenciaSQL, conexion))
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
